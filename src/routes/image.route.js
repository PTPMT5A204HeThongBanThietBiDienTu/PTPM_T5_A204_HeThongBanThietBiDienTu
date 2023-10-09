const { Router } = require("express")
const {
    getAllByProId,
    uploadGallery
} = require("../controllers/image.controller")
const { validateUpload } = require("../middlewares/handleUpload.middleware")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")

const router = Router()

router.get('/getAllByProId/:id', asyncHandle(getAllByProId))
router.post('/uploadGallery/:id', validateUpload, asyncHandle(uploadGallery))

module.exports = router