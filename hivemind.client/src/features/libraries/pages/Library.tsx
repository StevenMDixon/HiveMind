import Container from '@mui/material/Container';
import { useNavigate } from "react-router-dom";

import Header from '@/components/Header';
import CustomTable, { type CellData } from '@/components/CustomTable';

import type { Library } from '../types';

import { librariesQueryOptions, libraryTypesQueryOptions } from '../api/queryOptions';
import { useQuery } from '@tanstack/react-query';

import LibraryCreateDialog from '../components/LibraryCreateDialog';
import { useLibraryCreateHook, useLibraryDeleteHook, useLibraryRefreshHook } from '../hooks/libraryHooks';

const LibraryPage = () => {

    const { data: libraries, refetch: refetchLibraries, isLoading } = useQuery(librariesQueryOptions());
    const { data: libraryTypes } = useQuery(libraryTypesQueryOptions());

    const navigate = useNavigate();

    const { mutate: createLibrary } = useLibraryCreateHook();
    const { mutate: deleteLibrary } = useLibraryDeleteHook();
    const { mutate: refreshLibrary } = useLibraryRefreshHook()
    
    const handleCreateLibrary = async (libraryInputs : Library)  => {
        createLibrary(libraryInputs);
    };

    const handleDeleteLibrary = async (libraryId: number) => {
        deleteLibrary(libraryId)
    };

    const handleRefreshLibrary = async (libraryId: number) => {
        refreshLibrary(libraryId)
    }

    const columns = [
        { key: 'libraryId', name: 'ID', align: 'left' },
        { key: 'libraryName', name: 'Name', align: 'left' },
        { key: 'libraryType', name: 'Type', align: 'left' },
        { key: 'libraryPath', name: 'Path', align: 'left' }
    ] as CellData<Library>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", align: 'center', action: (e) => navigate("/libraries/" + e.libraryId), icon: "Edit"  },
        { key: 'a2', name: "Delete", align: 'center', action: (e) => handleDeleteLibrary(e.libraryId), icon: "Delete" },
        { key: 'a3', name: "Refresh", align: 'center', action: (e) => handleRefreshLibrary(e.libraryId), icon: "Refresh",  disabled: (e: Library) => !e.isProcessed},

    ] as CellData<Library>[];

    const handleRetry = () => {
        refetchLibraries();
    };

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Libraries">
                {libraryTypes && <LibraryCreateDialog createLibrary={handleCreateLibrary} libraryTypes={libraryTypes} />}
            </Header>
            <CustomTable data={libraries} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry} isLoading={isLoading}></CustomTable>
        </Container>
    )
}

export default LibraryPage;