import styles from "./spinner.module.css";

export const Spinner = () => {
    return (
        <div className={styles.spinner}>
            <div className={styles.loader} />
            <h2 className={styles.title}>
                Loading...
            </h2>
        </div>
    );
}