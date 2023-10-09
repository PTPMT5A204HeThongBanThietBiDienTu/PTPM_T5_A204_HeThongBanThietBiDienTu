const { Router } = require("express")
const {
    getAll,
    getById,
    create,
    update,
    remove
} = require("../controllers/role.controller")
const { validateBody } = require("../middlewares/validate.middleware")
const { validateRole } = require("../utils/validations")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")

const router = Router()

router.get('/', asyncHandle(getAll))
router.get('/:id', asyncHandle(getById))
router.post('/create', validateBody(validateRole), asyncHandle(create))
router.patch('/:id', validateBody(validateRole), asyncHandle(update))
router.delete('/:id', asyncHandle(remove))

module.exports = router