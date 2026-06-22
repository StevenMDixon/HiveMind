import { useParams, useNavigate } from "react-router-dom";

import Container from '@mui/material/Container';
import Toolbar from '@mui/material/Toolbar';
import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button'

import { QueryLayout } from '../components/QueryLayout'
import { QueryResultsLayout } from '../components/QueryResultsLayout';
import type { Query } from '../types'

import { useQuery } from '@tanstack/react-query';
import { queryQueryOptions, testQueryOptions } from '../api/queryOptions'

import { useQueryUpdateHook } from '../hooks/queryHooks';
 

const QueryDetail = () => {
    const { id } = useParams();

    const { data: query } = useQuery(queryQueryOptions(Number(id ?? 0)));
    const { data: testResults } = useQuery(testQueryOptions(Number(id ?? 0)));

    const navigate = useNavigate();

    const { mutate: saveQuery } = useQueryUpdateHook();

    const handleSaveQuery = async (query: Query) => {
        saveQuery(query);
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
            
            {query && <QueryLayout query={query} save={handleSaveQuery} />}       
            {testResults && <QueryResultsLayout testResults={testResults} />}
        </Container>
    )
}

export default QueryDetail;