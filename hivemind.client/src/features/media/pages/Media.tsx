import Container from '@mui/material/Container';
import Header from '@/components/Header';
import CustomTable, { type CellData } from "@/components/CustomTable";

import type { Media, Show } from '../types';

import { useQuery } from '@tanstack/react-query';
import { mediaQueryOptions } from '../api/queryOptions';

const MediaPage = () => {
    const { data: media, refetch: refectchMedia, isLoading } = useQuery(mediaQueryOptions());

    const handleRetry = () => {
        refectchMedia();
    };

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
            <CustomTable data={media} columns={columns} handleRetry={handleRetry} isLoading={isLoading} />
         </Container>
    )
}

export default MediaPage;