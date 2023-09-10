import roleRouter from "./role.route"

const useRouter = (app) => {
    app.use('/role', roleRouter)
}

export default useRouter