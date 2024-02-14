import { useState } from "react";
import { Modal } from "../components/UI/Modal/Modal";

const HomePage = () => {
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

    const handleOpenModal = () => {
        setIsModalOpen(true);
    };

    const handleCloseModal = () => {
        setIsModalOpen(false);
    };

    return (
        <div className="content">
            <p>This is the main content area.</p>
            <button onClick={handleOpenModal}>
                Modal
            </button>
            <Modal visible={isModalOpen} title="New Board" onClose={handleCloseModal}>
            {/*Hello world*/}
            </Modal>
        </div>
    );
}

export default HomePage;