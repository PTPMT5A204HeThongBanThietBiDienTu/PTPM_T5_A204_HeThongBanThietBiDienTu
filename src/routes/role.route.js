import { Router } from "express"
import {
    getAll,
    getById,
    create,
    update,
    remove
} from "../controllers/role.controller"
import { validateBody } from "../middlewares/validate.middleware"
import { validateRole } from "../utils/validations"

const router = Router()

router.get('/', getAll)
router.get('/:id', getById)
router.post('/create', create)
router.patch('/:id', validateBody(validateRole), update)
router.delete('/:id', remove)

export default router