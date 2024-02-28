import styles from "./modal.module.css";

interface IModalProps {
    isVisible: boolean,
    onClose: () => void;
    children?: React.ReactNode;
}

export const Modal: React.FC<IModalProps> = ({ isVisible, onClose, children }) => {
    return (
        <div className={`${styles.overlay} ${isVisible && styles.open}`} onClick={onClose}>
            <div className={styles.modal} onClick={e => e.stopPropagation()}>
                {children}
            </div>
        </div>
    );
}