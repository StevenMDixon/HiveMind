import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import Toolbar from '@mui/material/Toolbar';
import AppBar from '@mui/material/AppBar';

import TableContainer from '@mui/material/TableContainer';
import Table from '@mui/material/Table';
import TableHead from '@mui/material/TableHead';
import TableBody from '@mui/material/TableBody';
import TableRow from '@mui/material/TableRow';
import TableCell from '@mui/material/TableCell';
//import TableFooter from '@mui/material/TableFooter';
//import TablePagination from '@mui/material/TablePagination';
import Paper from '@mui/material/Paper';

//import RoundedLoadingFiller from './Components/RoundedLoadingFiller';

import { useState, Suspense, use } from 'react';
import ErrorBoundary from '../../Components/ErrorBoundary';


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

const MediaTableBody = ({ promise }: { promise: Promise<Media[]> }) => {
    const mediaItems = use(promise);

    return (
        <>
            {mediaItems.map((item: Media) => (
                <TableRow
                    key={item.mediaItemId}
                    sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                >
                    <TableCell component="th" scope="row">
                        {item.mediaItemId}
                    </TableCell>
                    <TableCell align="left">{item.title}</TableCell>
                    <TableCell align="left">{item.mediaType}</TableCell>
                    <TableCell align="right">{(item.duration / 1000 / 60).toFixed(2) + " (m)"}</TableCell>
                    <TableCell align="right">{item.width + " px"}</TableCell>
                    <TableCell align="right">{item.height + " px"}</TableCell>
                    <TableCell align="right">{item.show ? item.show.name : ""}</TableCell>
                    <TableCell align="right">{item.episodeNumber}</TableCell>
                    <TableCell align="right">{item.seasonNumber}</TableCell>
                    <TableCell align="right">{item.tags ? item.tags.map(x => x.tagName).join(", ") : ""}</TableCell>
                </TableRow>
            ))}
        </>
    );
};

const MediaPage = () => {
    const [mediaPromise, setMediaPromise] = useState(() => fetchMedia());

    const handleRetry = () => {
        setMediaPromise(fetchMedia());
    };

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Media
                    </Typography>
                </Toolbar>
            </AppBar>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                    <TableHead>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell align="center">Title</TableCell>
                            <TableCell align="center">Type</TableCell>
                            <TableCell align="right">Duration</TableCell>
                            <TableCell align="right">Width</TableCell>
                            <TableCell align="right">Height</TableCell>
                            <TableCell align="center">Show</TableCell>
                            <TableCell align="center">Episode</TableCell>
                            <TableCell align="center">Season</TableCell>
                            <TableCell align="center">Tags</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        <ErrorBoundary
                            fallback={(error, reset) => (
                                <TableRow>
                                    <TableCell colSpan={10} align="center">
                                        <Typography sx={{ mb: 1 }}>
                                            {error.message}
                                        </Typography>
                                        <button onClick={() => { reset(); handleRetry(); }}>
                                            Retry
                                        </button>
                                    </TableCell>
                                </TableRow>
                            )}
                        >
                        <Suspense fallback={
                            <TableRow>
                                <TableCell colSpan={10} align="center">Loading...</TableCell>
                            </TableRow>
                        }>
                            <MediaTableBody promise={mediaPromise} />
                        </Suspense>
                    </ErrorBoundary>
                    </TableBody>
                </Table>
            </TableContainer>
        </Container>
    )
}

export default MediaPage;