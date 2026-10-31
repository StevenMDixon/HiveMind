import Container from '@mui/material/Container';
import { useState } from 'react';
import CustomTable, { type CellData, type PagingData } from '@/components/CustomTable';
import Header from '@/components/Header';
import CustomDialog from '@/components/Dialog';
import { type CustomFormField } from '@/components/FormFields';

import { useNavigate } from "react-router-dom";

import type { Query } from '../types'

import { useQuery } from '@tanstack/react-query';
import { queryQuerySearchOptions } from '../api/queryOptions';
import { useQueryCreateHook, useQueryDeleteHook } from '../hooks/queryHooks';

const QueryPage = () => {
    const [search, setSearch] = useState('');
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize] = useState(18);

    const { data: queries, refetch: refectchQueries, isLoading } = useQuery(queryQuerySearchOptions(search, pageIndex, pageSize));

    const { mutate: createQuery } = useQueryCreateHook();
    const { mutate: deleteQuery } = useQueryDeleteHook();

    const navigate = useNavigate();

    const queryDefault = { name: '' } as Query;

    const handleCreateQuery = async (query: Query) => {
        createQuery(query)
    };

    const handleDeleteQuery = async (queryId: number) => {
        deleteQuery(queryId);
    };


    const columns = [
        { key: 'id', name: 'ID', align: 'left' },
        { key: 'name', name: 'Query Name', align: 'left' },
        { key: 'type', name: 'Type', align: 'left' },
    ] as CellData<Query>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", action: (e: Query) => navigate("/queries/" + e.id), icon: "Edit" },
        { key: 'a2', name: "Delete", action: (e: Query) => handleDeleteQuery(e.id), icon: "Delete" }
    ] as CellData<Query>[];

    const handleRetry = () => {
        refectchQueries()
    };

    const Search = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setSearch(e.target.value);
    }
    
    const HandlePageIndexChange = (_: React.ChangeEvent<unknown>, value: number) => {
        console.log(value);
        setPageIndex(value);
    }

    const paging = {
        currentPage: pageIndex,
        dataTotal: queries?.dataCount,
        pageSize: pageSize,
        changePage: HandlePageIndexChange
     } as PagingData;   

    const fields = [
        { name: 'queryName', display: "Name", type: "Text", initialValue: queryDefault.name, validator: (queryName: string) => queryName != '', required: true },
    ] as CustomFormField[];

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Queries">
                <CustomDialog buttonText="Add Query" title="Create Query" fields={fields} initialValue={queryDefault} save={handleCreateQuery} />
            </Header>
            <CustomTable data={queries?.data} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry} isLoading={isLoading} searchFN={Search} paging={paging} />
        </Container>
    )
}

export default QueryPage;