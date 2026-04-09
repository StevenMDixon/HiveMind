import Container from '@mui/material/Container';

import { useState } from 'react';

import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';

import CustomTable, { type CellData } from '../Components/Table';

import Header from '../Components/Header';
import CustomDialog from '../Components/Dialog';
import { type CustomFormField } from '../Components/FormFields';

import { useNavigate } from "react-router-dom";

import type { Collection } from '../../Types/Collections'

import { fetchCollections, createCollection, deleteCollection } from '../../Api/Collections';

const CollectionPage = () => {
    const { showNotification } = useGlobalNotification();

    const [collectionPromise, setCollectionPromise] = useState(() => fetchCollections());

    const navigate = useNavigate();

    const collectionDefault = {collectionName: ''} as Collection;

    const handleCreateCollection = async (collection: Collection) => {
        const response = await createCollection(collection);

        if (response.ok) {
            showNotification("Collection Created", "success");
            const data = await response.json();
            navigate("/collection/" + data.collectionId)
        } else {
            showNotification("Failed to create collection", "error");
        }
    };

    const handleDeleteCollection = async (collectionId: number) => {
        const response = await deleteCollection(collectionId);

        if (response.ok) {
            showNotification("Collection deleted", "success")
        } else {
            showNotification("Failed to delete collection", "error");
        }

        setCollectionPromise(fetchCollections());
    };


    const columns = [
        { key: 'collectionID', name: 'ID', align: 'left' },
        { key: 'collectionName', name: 'Collection Name', align: 'left' }
    ] as CellData<Collection>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", action: (e: Collection) => navigate("/collection/" + e.collectionID), icon: "Edit" },
        { key: 'a2', name: "Delete", action: (e: Collection) => handleDeleteCollection(e.collectionID), icon: "Delete" }
    ] as CellData<Collection>[];

    const handleRetry = () => {
        setCollectionPromise(fetchCollections());
    };

    const fields = [
        { name: 'collectionName', type: "Text", initialValue: collectionDefault.collectionName, validator: (collectionName: string) => collectionName != '', required: true },
    ] as CustomFormField[];


    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Collections">
                <CustomDialog buttonText="Add Collection" title="Create Collection" fields={fields} initialValue={collectionDefault} save={handleCreateCollection} />
            </Header>
            <CustomTable dataPromise={collectionPromise} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry} />
        </Container>
    )
}

export default CollectionPage;