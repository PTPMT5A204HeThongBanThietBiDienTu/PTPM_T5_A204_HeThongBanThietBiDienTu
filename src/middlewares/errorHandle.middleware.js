const errorHandle = (err, req, res, next) => {
    return res.status(400).json({
        success: false,
        message: err.errors ? err.errors[0].message : err?.message
    })
}

const asyncHandle = (controller) => {
    return (req, res, next) => {
        controller(req, res, next).catch(next)
    }
}

module.exports = {
    errorHandle,
    asyncHandle
}