import Container from '@mui/material/Container';
import { useState, use, Suspense } from 'react';
import { useNavigate } from "react-router-dom";

import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';
import Header from '../Components/Header';
import CustomTable, { type CellData } from '../Components/Table';


import type { Library } from '../../Types/Library';
import { type CustomFormField } from '../Components/FormFields';

import CustomDialog from '../Components/Dialog';

import { ApiClient } from '../../Api/ApiClient';
interface NewLibraryFormProps {
    createLibrary: (a: Library) => void;
    libraryTypesPromise: Promise<string[]>;
}

const NewLibraryForm = ({createLibrary, libraryTypesPromise } : NewLibraryFormProps) => {

    const libraryTypes = use(libraryTypesPromise);

    const libraryDefault = { libraryId: 0, libraryName: '', libraryPath: '', libraryType: 0 };

    const fields = [
        { name: 'libraryName', display: "Name", type: "Text", initialValue: libraryDefault.libraryName, validator: (libraryName: string) => libraryName != '', required: true },
        { name: 'libraryPath', display: "Path", type: "Text", initialValue: libraryDefault.libraryPath, validator: (libraryPath: string) => libraryPath != '', required: true },
        { name: 'libraryType', display: "Type", type: "Select", initialValue: libraryDefault.libraryType, required: true, options: libraryTypes }

    ] as CustomFormField[];

    return (
            <CustomDialog buttonText="Add Library" title="Create Library" save={createLibrary} fields={fields} initialValue={libraryDefault} />
    )
}

const LibraryPage = () => {
    const { showNotification } = useGlobalNotification();

    const [librariesPromise, setLibrariesPromise] = useState(() => ApiClient.fetchLibraries());
    const [libraryTypesPromise] = useState(() => ApiClient.fetchLibraryTypes());

    const navigate = useNavigate();
    
    const handleCreateLibrary = async (libraryInputs : Library)  => {
        const response = await ApiClient.createLibrary(libraryInputs);

        if (response.ok) {
            showNotification("Library Created", "success");
        } else {
            showNotification("Failed to create library", "error");
        }

        setLibrariesPromise(ApiClient.fetchLibraries());
    };

    const handleDeleteLibrary = async (libraryId: number) => {
        const response = await ApiClient.deleteLibrary(libraryId);

        if (response.ok) {
            showNotification("Library deleted", "success")
        } else {
            showNotification("Failed to delete library", "error");
        }

        setLibrariesPromise(ApiClient.fetchLibraries());
    };

    const columns = [
        { key: 'libraryId', name: 'ID', align: 'left' },
        { key: 'libraryName', name: 'Name', align: 'left' },
        { key: 'libraryType', name: 'Type', align: 'left' },
        { key: 'libraryPath', name: 'Path', align: 'left' }
    ] as CellData<Library>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", align: 'center', action: (e) => navigate("/libraries/" + e.libraryId), icon: "Edit"  },
        { key: 'a2', name: "Delete", align: 'center', action: (e) => handleDeleteLibrary(e.libraryId), icon: "Delete" }
    ] as CellData<Library>[];

    const handleRetry = () => {
        setLibrariesPromise(ApiClient.fetchLibraries());
    };

    

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Libraries">
                <Suspense>
                    <NewLibraryForm createLibrary={handleCreateLibrary} libraryTypesPromise={libraryTypesPromise} />
                </Suspense>
            </Header>
            <CustomTable dataPromise={librariesPromise} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry}></CustomTable>
        </Container>
    )
}

export default LibraryPage;