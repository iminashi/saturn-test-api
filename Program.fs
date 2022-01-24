open Saturn
open Giraffe

let getAll : HttpHandler =
    fun next ctx ->
        task {
            let! data = Db.getAll ()
            return! json data next ctx
        }

let apiRouter = router {
    not_found_handler (setStatusCode 404 >=> text "Not found.")
    get "/" getAll
}

let app = application {
    use_gzip
    use_router apiRouter
}

run app
