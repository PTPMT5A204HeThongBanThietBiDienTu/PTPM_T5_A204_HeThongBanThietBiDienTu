const { Router } = require("express")
const {
    getAll,
    getById,
    create,
    update,
    remove
} = require("../controllers/brand.controller")
const { validateBody } = require("../middlewares/validate.middleware")
const { validateBrand } = require("../utils/validations")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")

const router = Router()

router.get('/', asyncHandle(getAll))
router.get('/:id', asyncHandle(getById))
router.post('/create', validateBody(validateBrand), asyncHandle(create))
router.patch('/:id', validateBody(validateBrand), asyncHandle(update))
router.delete('/:id', asyncHandle(remove))

module.exports = router