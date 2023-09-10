import express from "express"

const useMiddleware = (app) => {
    app.use(express.json())
    app.use(express.urlencoded({ extended: true }))
}

export default useMiddleware