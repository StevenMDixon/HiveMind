import Container from '@mui/material/Container';
import { useNavigate } from "react-router-dom";
import { useState } from 'react';
import Header from '@/components/Header';
import CustomTable, { type CellData, type PagingData } from '@/components/CustomTable';

import type { Library } from '../types';

import { libraryQuerySearchOptions, libraryTypesQueryOptions } from '../api/queryOptions';
import { useQuery } from '@tanstack/react-query';

import LibraryCreateDialog from '../components/LibraryCreateDialog';
import { useLibraryCreateHook, useLibraryDeleteHook, useLibraryRefreshHook } from '../hooks/libraryHooks';

const LibraryPage = () => {
    const [search, setSearch] = useState('');
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize] = useState(18);


    const { data: libraries, refetch: refetchLibraries, isLoading } = useQuery(libraryQuerySearchOptions(search, pageIndex, pageSize));
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
        { key: 'id', name: 'ID', align: 'left' },
        { key: 'name', name: 'Name', align: 'left' },
        { key: 'type', name: 'Type', align: 'left' },
        { key: 'path', name: 'Path', align: 'left' }
    ] as CellData<Library>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", align: 'center', action: (e) => navigate("/libraries/" + e.id), icon: "Edit"  },
        { key: 'a2', name: "Delete", align: 'center', action: (e) => handleDeleteLibrary(e.id), icon: "Delete" },
        { key: 'a3', name: "Refresh", align: 'center', action: (e) => handleRefreshLibrary(e.id), icon: "Refresh",  disabled: (e: Library) => !e.isProcessed},

    ] as CellData<Library>[];

    const handleRetry = () => {
        refetchLibraries();
    };

    const Search = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setSearch(e.target.value);
    }
    
    const HandlePageIndexChange = (_: React.ChangeEvent<unknown>, value: number) => {
        setPageIndex(value);
    }

    const paging = {
        currentPage: pageIndex,
        dataTotal: libraries?.dataCount,
        pageSize: pageSize,
        changePage: HandlePageIndexChange
     } as PagingData;   

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Libraries">
                {libraryTypes && <LibraryCreateDialog createLibrary={handleCreateLibrary} libraryTypes={libraryTypes} />}
            </Header>
            <CustomTable data={libraries?.data} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry} isLoading={isLoading} searchFN={Search} paging={paging}></CustomTable>
        </Container>
    )
}

export default LibraryPage;