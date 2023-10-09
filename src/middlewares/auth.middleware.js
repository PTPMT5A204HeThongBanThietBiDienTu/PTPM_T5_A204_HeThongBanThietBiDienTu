const JWTService = require("../services/jwt.service")
const authenticate = (req, res, next) => {
    let token = req.session.userToken?.accessToken
    if (!token) {
        return res.status(401).json({
            success: false,
            message: 'Unauthorized'
        })
    }

    const decode = JWTService.decodeAccessToken(token)
    req.user = decode
    return next()
}

module.exports = authenticate