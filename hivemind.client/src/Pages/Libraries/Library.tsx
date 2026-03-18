import Container from '@mui/material/Container';
import Button from '@mui/material/Button';

import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import TextField from '@mui/material/TextField';
import { useState, use, Suspense } from 'react';

import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';
import Header from '../Components/Header';
import CustomTable, { type CellData } from '../Components/Table';
import { Select, MenuItem, FormControl } from '@mui/material';
import type { SelectChangeEvent } from '@mui/material';

interface Library {
    libraryId: number;
    libraryName: string;
    libraryPath: string;
    libraryType: string
}

const fetchLibraries = async () => {
    const response = await fetch('/libraries');
    if (response.ok) {
        const data = await response.json();
        return data.libraries
    }
    return [];
};

const fetchLibraryTypes = async () => {
    const response = await fetch('/libraries/types');
    if (response.ok) {
        const data = await response.json();
        return data.types
    }
    return [];
};

interface NewLibraryFormProps {
    handleModalClose: () => void;
    createModalState: boolean;
    createLibrary: (a: Library, b: () => void ) => void;
    libraryTypesPromise: Promise<string[]>
}

const NewLibraryForm = ({ handleModalClose, createModalState, createLibrary, libraryTypesPromise } : NewLibraryFormProps) => {

    const libaryTypes = use(libraryTypesPromise);

    const librarylInputDefault = { libraryId: 0, libraryName: "", libraryPath: "", libraryType: "None" }

    const [libraryInputs, setLibraryInputs] = useState<Library>(librarylInputDefault)

    const updateLibraryInputs = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement> | SelectChangeEvent<string>) => {
        const { name, value } = event.target;
        setLibraryInputs(values => ({ ...values, [name]: value }))
    }

    const clearForm = () => setLibraryInputs(librarylInputDefault);

    return (
        <Dialog onClose={handleModalClose} open={createModalState}>
            <DialogTitle>Create Library</DialogTitle>
            <DialogContent>
                <FormControl>
                <TextField
                        required
                        name="libraryName"
                        label="Library Name"
                        value={libraryInputs.libraryName}
                        onChange={updateLibraryInputs}
                        sx={{m: 1}}
                />
                <TextField
                    required
                    name="libraryPath"
                    label="Library Path"
                    value={libraryInputs.libraryPath}
                    onChange={updateLibraryInputs}
                    sx={{ m: 1 }}
                />
                <Select
                    name="libraryType"
                    value={libraryInputs.libraryType}
                        label="Library Type"
                        labelId="demo-simple-select-label"
                        onChange={updateLibraryInputs}
                        sx={{ m: 1 }}
                    >
                    {libaryTypes.map((type: string, index: number) => (<MenuItem value={index}>{type}</MenuItem>))}
                    </Select>
                    </FormControl>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => createLibrary(libraryInputs, clearForm)}>Create</Button>
            </DialogActions>
        </Dialog>
    )
}

const LibraryPage = () => {
    const [createModalState, setCreateModalState] = useState<boolean>(false);
    const handleModalOpen = () => setCreateModalState(true);
    const handleModalClose = () => setCreateModalState(false);

    const { showNotification } = useGlobalNotification();

    const [librariesPromise, setLibrariesPromise] = useState(() => fetchLibraries());
    const [libraryTypesPromise] = useState(() => fetchLibraryTypes());
    
    const createLibrary = async (libraryInputs : Library, clearForm: () => void)  => {
        const response = await fetch('/libraries', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ LibraryName: libraryInputs.libraryName, LibraryPath: libraryInputs.libraryPath, LibraryType: libraryInputs.libraryType }),
        });

        if (response.ok) {
            clearForm();
            showNotification("Library Created", "success");
        } else {
            showNotification("Failed to create library", "error");
        }

        handleModalClose();
        setLibrariesPromise(fetchLibraries());
    };

    const deleteLibrary = async (libraryId: number) => {
        const response = await fetch(`/libraries/${libraryId}`, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        });

        if (response.ok) {
            showNotification("Library deleted", "success")
        } else {
            showNotification("Failed to delete library", "error");
        }

        setLibrariesPromise(fetchLibraries());
    };

    const columns = [
        { key: 'libraryId', name: 'ID', align: 'left' },
        { key: 'libraryName', name: 'Name', align: 'left' },
        { key: 'libraryType', name: 'Type', align: 'left' },
        { key: 'libraryPath', name: 'Path', align: 'left' }
    ] as CellData[];

    const actionColumns = [
        { key: 'a1', name: "Edit", align: 'center', action: (e: Library) => console.log(e), icon: "Edit"  },
        { key: 'a2', name: "Delete", align: 'center', action: (e: Library) => deleteLibrary(e.libraryId), icon: "Delete" }
    ] as CellData[];

    const handleRetry = () => {
        setLibrariesPromise(fetchLibraries());
    };

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Libraries">
                <Button onClick={handleModalOpen}>Add Library</Button>
            </Header>
            <CustomTable dataPromise={librariesPromise} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry}></CustomTable>
            <Suspense>
                <NewLibraryForm createModalState={createModalState} handleModalClose={handleModalClose} createLibrary={createLibrary} libraryTypesPromise={libraryTypesPromise} />
            </Suspense>
        </Container>
    )
}

export default LibraryPage;