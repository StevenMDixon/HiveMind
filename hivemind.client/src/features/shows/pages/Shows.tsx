import Container from '@mui/material/Container';
import Header from '@/components/Header';
import CustomTable, { type CellData, type PagingData } from '@/components/CustomTable';
import { useNavigate } from "react-router-dom";

import type { Show } from '../types';
import { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { showQuerySearchOptions } from '../api/queryOptions';

const ShowsPage = () => {
    const [search, setSearch] = useState('');
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize] = useState(18);
    
    const navigate = useNavigate();

    const { data: shows, refetch, isLoading } = useQuery(showQuerySearchOptions(search, pageIndex, pageSize));

    const columns = [
        { key: 'id', name: 'ID', align: 'left' },
        { key: 'name', name: 'Show Name', align: 'left' },
        { key: 'rating', name: 'TV Rating', align: 'left', format: (s) => s.replace("_", " ") },
    ] as CellData<Show>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", align: 'center', action: (e) => navigate("/shows/" + e.id), icon: "Edit"  }
    ] as CellData<Show>[];

    const handleRetry = () => {
        refetch(); 
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
        dataTotal: shows?.dataCount,
        pageSize: pageSize,
        changePage: HandlePageIndexChange
     } as PagingData;   

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Shows"/>
            <CustomTable data={shows?.data} handleRetry={handleRetry} columns={columns} actionColumns={actionColumns} isLoading={isLoading} searchFN={Search} paging={paging} />
        </Container>
    )
}

export default ShowsPage;