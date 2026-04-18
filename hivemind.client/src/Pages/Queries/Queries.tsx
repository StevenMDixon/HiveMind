import Container from '@mui/material/Container';

import { useState } from 'react';

import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';

import CustomTable, { type CellData } from '../Components/Table';

import Header from '../Components/Header';
import CustomDialog from '../Components/Dialog';
import { type CustomFormField } from '../Components/FormFields';

import { useNavigate } from "react-router-dom";

import type { Query } from '../../Types/Query'

import { ApiClient } from '../../Api/ApiClient';

const QueryPage = () => {
    const { showNotification } = useGlobalNotification();

    const [queryPromise, setQueryPromise] = useState(() => ApiClient.fetchQueries());

    const navigate = useNavigate();

    const queryDefault = { queryName: '' } as Query;

    const handleCreateQuery = async (query: Query) => {
        const response = await ApiClient.createQuery(query);

        if (response.ok) {
            showNotification("Query Created", "success");
            const data = await response.json();
            navigate("/queries/" + data.queryId)
        } else {
            showNotification("Failed to create query", "error");
        }
    };

    const handleDeleteQuery = async (queryId: number) => {
        const response = await ApiClient.deleteQuery(queryId);

        if (response.ok) {
            showNotification("Query deleted", "success")
        } else {
            showNotification("Failed to delete query", "error");
        }

        setQueryPromise(ApiClient.fetchQueries());
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
        setQueryPromise(ApiClient.fetchQueries());
    };

    const fields = [
        { name: 'queryName', display: "Name", type: "Text", initialValue: queryDefault.queryName, validator: (queryName: string) => queryName != '', required: true },
    ] as CustomFormField[];


    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Queries">
                <CustomDialog buttonText="Add Query" title="Create Query" fields={fields} initialValue={queryDefault} save={handleCreateQuery} />
            </Header>
            <CustomTable dataPromise={queryPromise} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry} />
        </Container>
    )
}

export default QueryPage;