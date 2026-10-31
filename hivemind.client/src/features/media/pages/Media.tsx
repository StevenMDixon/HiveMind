import Container from '@mui/material/Container';
import { useState } from 'react';
import Header from '@/components/Header';
import CustomTable, { type CellData, type PagingData } from "@/components/CustomTable";

import type { Media, Show } from '../types';

import { useQuery } from '@tanstack/react-query';
import { mediaQuerySearchOptions } from '../api/queryOptions';

const MediaPage = () => {

    const [search, setSearch] = useState('');
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize] = useState(18);
        

    const { data: media, refetch: refetchMedia, isLoading } = useQuery(mediaQuerySearchOptions(search, pageIndex, pageSize));

    const handleRetry = () => {
        refetchMedia();
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
        dataTotal: media?.dataCount,
        pageSize: pageSize,
        changePage: HandlePageIndexChange
     } as PagingData;   

    const columns = [
        { key: 'title', name: 'ID', align: 'left' },
        { key: 'mediaType', name: 'Type', align: 'center' },
        { key: 'duration', name: 'Duration', align: 'right', format: (duration: number) => (duration /1000/ 60).toFixed(5) + ' (M)' },
        { key: 'width', name: 'Width', align: 'right', format: (width: string) => width + " px" },
        { key: 'height', name: 'Height', align: 'right', format: (height: string) => height + " px" },
        { key: 'show', name: 'Show', align: 'center', format: (show: Show) => (show?.name)},
        { key: 'episodeNumber', name: 'Episode', align: 'center' },
        { key: 'seasonNumber', name: 'Season', align: 'center' },
    ] as CellData<Media>[];

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Media">
            </Header>
            <CustomTable data={media?.data} columns={columns} handleRetry={handleRetry} isLoading={isLoading} searchFN={Search} paging={paging}/>
         </Container>
    )
}

export default MediaPage;