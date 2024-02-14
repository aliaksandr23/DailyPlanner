import { IoClose } from "react-icons/io5";
import styles from "./modal.module.css";
interface IModalProps {
    title?: string,
    visible: boolean,
    onClose: () => void;
    children?: React.ReactNode;
}

export const Modal: React.FC<IModalProps> = ({ title, visible, onClose, children }) => {
    const handleClose = () => {
        onClose();
    }

    return (
        <div className={`${styles.overlay} ${visible && styles.open}`} onClick={handleClose}>
            <div className={styles.modal} onClick={e => e.stopPropagation()}>
                <div className={styles.header} >
                    <h2>{title}</h2>
                    <IoClose className={styles.close} onClick={handleClose} />
                </div>
                <div className="modal-body">
                    {children}
                </div>
            </div>
        </div>
    );
}