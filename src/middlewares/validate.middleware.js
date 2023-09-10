export const validateBody = (validate) => {
    const middleware = (req, res, next) => {
        const valid = validate(req.body)
        if (valid.error) {
            return res.status(400).json({
                success: false,
                message: valid.error.details[0].message
            })
        }
        next()
    }
    return middleware
}