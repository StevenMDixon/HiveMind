import { useParams, useNavigate } from "react-router-dom";
import { useState, Suspense} from 'react'
import ErrorBoundary from '../../Components/ErrorBoundary';

import Container from '@mui/material/Container';
import Toolbar from '@mui/material/Toolbar';
import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button'

import { QueryLayout } from './QueryLayout'
import { QueryResultsLayout } from './QueryResultsLayout'
import type { Query } from '../../Types/Query'

import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';
import { ApiClient } from '../../Api/ApiClient';

const QueryDetail = () => {
    const { id } = useParams();

    const navigate = useNavigate();

    const [queryPromise] = useState(() => ApiClient.fetchQuery(Number(id)));
    const [optionPromise] = useState(() => ApiClient.fetchOptions());
    const [settingPromise] = useState(() => ApiClient.fetchSettings());
    const [queryResultsTestPromise, setResultPromise] = useState(() => ApiClient.fetchTestQueryResults(Number(id)))

    const { showNotification } = useGlobalNotification();

    const handleSaveQuery = async (query: Query) => {
        console.log(query)
        const result = await ApiClient.saveQuery(query);

        if (result.ok) {
            showNotification("Query Saved Successfully", "success")
        } else {
            showNotification("Error Editing Query", "error")
        }

        setResultPromise(ApiClient.fetchTestQueryResults(Number(id)))
    }

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Query Details
                    </Typography>
                    <Button onClick={()=> navigate(-1) }> Back</Button>
                </Toolbar>
            </AppBar>
            <ErrorBoundary
                fallback={(error) => (
                    <p>{error.message}</p>
                )}>
                <Suspense fallback={<p> Loading </p>}>
                    <QueryLayout promise={queryPromise} options={optionPromise} settings={settingPromise} save={handleSaveQuery} />       
                </Suspense>
                
            </ErrorBoundary>
            <ErrorBoundary
                fallback={(error) => (
                    <p>{error.message}</p>
                    )}>
                    <Suspense>
                        <QueryResultsLayout testResultsPromise={queryResultsTestPromise} />
                    </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default QueryDetail;