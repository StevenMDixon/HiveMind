import Container from '@mui/material/Container';
import Header from '../Components/Header';

import { useState } from 'react';

//import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';

import CustomTable, { type CellData } from '../Components/Table';

import type { Show } from '../../Types/Show';

import { ApiClient } from '../../Api/ApiClient';

const ShowsPage = () => {

    const [showsPromise, setShowsPromise] = useState(() => ApiClient.fetchShows());

    //const navigate = useNavigate();

    const columns = [
        { key: 'showId', name: 'ID', align: 'left' },
        { key: 'showName', name: 'Show Name', align: 'left' },
    ] as CellData<Show>[];


    const handleRetry = () => {
        setShowsPromise(ApiClient.fetchShows());
    };

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Shows"/>

            <CustomTable dataPromise={showsPromise} handleRetry={handleRetry} columns={columns} />
        </Container>
    )
}

export default ShowsPage;