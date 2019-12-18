namespace Searching

open System.Collections.Generic
open System.Linq

type Product(id:int, name:string, description:string) =
    member this.ID = id
    member this.Name = name
    member this.Description = description

type Searcher() = 
    static member Levenshtein word1 word2 =
        let preprocess = fun (str : string) -> str.ToLower().ToCharArray()
        let chars1, chars2 = preprocess word1, preprocess word2
        let m, n = chars1.Length, chars2.Length
        let table : int[,] = Array2D.zeroCreate (m + 1) (n + 1)
        for i in 0..m do
            for j in 0..n do
                match i, j with
                | i, 0 -> table.[i, j] <- i
                | 0, j -> table.[i, j] <- j
                | _, _ ->
                    let delete = table.[i - 1, j] + 1
                    let insert = table.[i, j - 1] + 1
                    let substitute = 
                        if chars1.[i - 1] = chars2.[j - 1] 
                            then table.[i - 1, j - 1]
                            else table.[i - 1, j - 1] + 1
                    table.[i, j] <- List.min [delete; insert; substitute]
        table.[m, n]

    static member Search (tagsAsString : string) (products : IEnumerable<Product>) =
        let a = [|' '; '+'; '='; ','; '.'; '!'; '@'; '#'; '$'; '%'; '^'; '&'; '*'; '('; ')'; '-'; '_';
            '|'; '\\'; '"'; '№'; ';'; ':'; '?'; '/'; '~'|]
        let tags = tagsAsString.Split(a).Distinct().Where(fun(x) -> x.Length > 0)
        products
            .Where(fun(prod) ->
                tags.All(fun(tag) ->
                    prod.Name.Split().Any(fun(word) -> Searcher.Levenshtein tag word <= (tag.Length - 1) / 3)))
            .Select(fun(prod) ->
                let sum = tags.Sum(fun(tag) ->
                    prod.Name.Split().Min(fun(word) ->
                        (float)(Searcher.Levenshtein tag word)))
                (prod.ID, sum))
            .OrderBy(fun(_, sum) -> sum)
            .Concat(products
                .Where(fun(prod) ->
                    tags.All(fun(tag) ->
                        prod.Name.Split().Any(fun(word) -> Searcher.Levenshtein tag word <= (tag.Length - 1) / 3)
                            || prod.Description.Split().Any(fun(word) -> Searcher.Levenshtein tag word <= (tag.Length - 1) / 3)))
                .Select(fun(prod) ->
                    let sum = tags.Sum(fun(tag) ->
                        let nameLev = prod.Name.Split().Min(fun(word) ->
                            (float)(Searcher.Levenshtein tag word))
                        let deskLev = prod.Description.Split().Min(fun(word) ->
                            (float)(Searcher.Levenshtein tag word) + 0.01)
                        if nameLev < deskLev then
                            nameLev
                        else deskLev)
                    (prod.ID, sum))
                .OrderBy(fun(_, sum) -> sum))
            (*.Concat(products
                .Where(fun(prod) ->
                    tags
                        .Take(1)
                        .Concat(tags.Where(fun(x) -> x.Length > 3))
                        .All(fun(tag) ->
                            prod.Name.Split().Any(fun(word) -> Searcher.Levenshtein tag word < 3)
                                || prod.Description.Split().Any(fun(word) -> Searcher.Levenshtein tag word < 3)))
                .Select(fun(prod) ->
                    let sum = tags.Sum(fun(tag) ->
                        let nameLev = prod.Name.Split().Min(fun(word) ->
                            (float)(Searcher.Levenshtein tag word))
                        let deskLev = prod.Description.Split().Min(fun(word) ->
                            (float)(Searcher.Levenshtein tag word) + 0.01)
                        if nameLev < deskLev then
                            nameLev
                        else deskLev)
                    (prod.ID, sum))
                .OrderBy(fun(_, sum) -> sum)) *)
            .Select(fun(id, _) -> id)
            .Distinct()
            