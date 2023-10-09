const { Router } = require("express")
const {
  getAll,
  getAllByCatId,
  getAllByBraId,
  getById,
  create,
  update,
  remove
} = require("../controllers/product.controller")
const { validateUpload } = require("../middlewares/handleUpload.middleware")
const { validateBody } = require("../middlewares/validate.middleware")
const { validateProduct } = require("../utils/validations")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")

const router = Router()

router.get('/', asyncHandle(getAll))
router.get('/:id', asyncHandle(getById))
router.get('/getByCatId/:id', asyncHandle(getAllByCatId))
router.get('/getByBraId/:id', asyncHandle(getAllByBraId))
router.post('/create', validateUpload, validateBody(validateProduct), asyncHandle(create))
router.patch('/:id', validateBody(validateProduct), asyncHandle(update))
router.delete('/:id', asyncHandle(remove))

module.exports = router