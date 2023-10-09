const multer = require("multer")
const storage = require("../config/imgStorage.config")
const imageFilter = require("../utils/validateUpload")

const validateUpload = (req, res, next) => {
    let uploadMultipleFiles = multer({ storage: storage, fileFilter: imageFilter }).array('upload_file', 8)

    uploadMultipleFiles(req, res, (err) => {
        let errMessage

        if (req.fileValidationError) {
            errMessage = req.fileValidationError
        }
        else if (!req.files || req.files.length === 0) {
            errMessage = 'Please select an image to upload'
        }
        else if (err instanceof multer.MulterError) {
            errMessage = 'The number of files exceeds the allowed limit'
        }
        else if (err) {
            errMessage = err
        }

        if (errMessage) {
            return res.status(400).json({
                success: false,
                message: errMessage
            })
        }
        else
            next()
    })
}

module.exports = {
    validateUpload
}