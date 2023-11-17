const { Router } = require("express")
const {
    getAllByUserId,
    create,
    update,
    remove,
    removeByUserId
} = require("../controllers/cart.controller")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")
const authenticate = require("../middlewares/auth.middleware")
const { validateBody } = require("../middlewares/validate.middleware")
const {
    validateInsertCart,
    validateUpdateCart
} = require("../utils/validations")

const router = Router()

router.get('/', authenticate, asyncHandle(getAllByUserId))
router.post('/create', authenticate, validateBody(validateInsertCart), asyncHandle(create))
router.patch('/:id', authenticate, validateBody(validateUpdateCart), asyncHandle(update))
router.delete('/deleteByUserId', authenticate, asyncHandle(removeByUserId))
router.delete('/:id', authenticate, asyncHandle(remove))

module.exports = router