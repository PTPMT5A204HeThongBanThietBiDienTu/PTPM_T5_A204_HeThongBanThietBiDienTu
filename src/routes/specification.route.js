const { Router } = require("express")
const {
    create,
    getAllByProId
} = require("../controllers/specification.controller")
const { validateBody } = require("../middlewares/validate.middleware")
const { validateSpecification } = require("../utils/validations")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")

const router = Router()

router.post('/create', validateBody(validateSpecification), asyncHandle(create))
router.get('/getByProId/:id', asyncHandle(getAllByProId))

module.exports = router