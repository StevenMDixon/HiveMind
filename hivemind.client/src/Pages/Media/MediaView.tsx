import Container from '@mui/material/Container';

//import RoundedLoadingFiller from './Components/RoundedLoadingFiller';
import { useState} from 'react';

import Header from '../Components/Header';

import CustomTable, { type CellData } from "../Components/Table";
/*import { useGlobalNotification } from '../Dashboard/useGlobalNotification';*/

interface Show {
    showId: number;
    name: string;
}
interface Tag {
    tagId: number;
    tagName: string;
}
interface Media {
    mediaItemId: number;
    title: string;
    duration: number;
    libraryId: number;
    mediaType: string;
    filePath: string;
    width: string;
    height: string;
    resolution: string;
    episodeNumber: number;
    seasonNumber: number;
    show: Show;
    tags: Tag[];
}

// Function that creates a new promise with error handling
const fetchMedia = async (): Promise<Media[]> => {
    const response = await fetch('/mediaitems');
    
    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }
    
    const data = await response.json();
    return data.mediaItems;
};

const MediaPage = () => {
    const [mediaPromise, setMediaPromise] = useState(() => fetchMedia());

    const handleRetry = () => {
        setMediaPromise(fetchMedia());
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
    ] as CellData[];

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Media">
            </Header>
            <CustomTable dataPromise={mediaPromise} columns={columns} handleRetry={handleRetry} />
         </Container>
    )
}

export default MediaPage;