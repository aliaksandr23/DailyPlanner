import styles from "./modal.module.css";
interface IModalProps {
    title?: string,
    visible: boolean,
    onClose: () => void;
    children?: React.ReactNode;
}

export const Modal: React.FC<IModalProps> = ({ visible, onClose, children }) => {
    return (
        <div className={`${styles.overlay} ${visible && styles.open}`} onClick={onClose}>
            <div className={styles.modal} onClick={e => e.stopPropagation()}>
                {children}
            </div>
        </div>
    );
}