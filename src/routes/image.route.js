const { Router } = require("express")
const {
    getAllByProId,
    uploadGallery,
    uploadImage
} = require("../controllers/image.controller")
const { validateUpload } = require("../middlewares/handleUpload.middleware")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")

const router = Router()

router.get('/getAllByProId/:id', asyncHandle(getAllByProId))
router.post('/uploadGallery/:id', validateUpload, asyncHandle(uploadGallery))
router.post('/uploadImage', validateUpload, asyncHandle(uploadImage))

module.exports = router