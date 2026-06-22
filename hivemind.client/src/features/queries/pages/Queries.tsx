import Container from '@mui/material/Container';

import CustomTable, { type CellData } from '@/components/CustomTable';
import Header from '@/components/Header';
import CustomDialog from '@/components/Dialog';
import { type CustomFormField } from '@/components/FormFields';

import { useNavigate } from "react-router-dom";

import type { Query } from '../types'

import { useQuery } from '@tanstack/react-query';
import { queriesQueryOptions } from '../api/queryOptions';
import { useQueryCreateHook, useQueryDeleteHook } from '../hooks/queryHooks';

const QueryPage = () => {
    const { data: queries, refetch: refectchQueries, isLoading } = useQuery(queriesQueryOptions());

    const { mutate: createQuery } = useQueryCreateHook();
    const { mutate: deleteQuery } = useQueryDeleteHook();

    const navigate = useNavigate();

    const queryDefault = { queryName: '' } as Query;

    const handleCreateQuery = async (query: Query) => {
        createQuery(query)
    };

    const handleDeleteQuery = async (queryId: number) => {
        deleteQuery(queryId);
    };


    const columns = [
        { key: 'queryId', name: 'ID', align: 'left' },
        { key: 'queryName', name: 'Query Name', align: 'left' }
    ] as CellData<Query>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", action: (e: Query) => navigate("/queries/" + e.queryId), icon: "Edit" },
        { key: 'a2', name: "Delete", action: (e: Query) => handleDeleteQuery(e.queryId), icon: "Delete" }
    ] as CellData<Query>[];

    const handleRetry = () => {
        refectchQueries()
    };

    const fields = [
        { name: 'queryName', display: "Name", type: "Text", initialValue: queryDefault.queryName, validator: (queryName: string) => queryName != '', required: true },
    ] as CustomFormField[];

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Queries">
                <CustomDialog buttonText="Add Query" title="Create Query" fields={fields} initialValue={queryDefault} save={handleCreateQuery} />
            </Header>
            <CustomTable data={queries} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry} isLoading={isLoading} />
        </Container>
    )
}

export default QueryPage;