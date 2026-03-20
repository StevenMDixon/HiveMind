import  { Container, AppBar, Toolbar, Typography, Button } from "@mui/material";
import { Suspense, useState } from "react";

import ErrorBoundary from "../../Components/ErrorBoundary";
import { useNavigate, useParams } from "react-router-dom";

import ChannelEditLayout from './ChannelEditLayout';
import type { Channel } from './types';

const fetchChannel = async (id: string | undefined): Promise<Channel>  => {
    const response = await fetch('/channels/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch channel: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    console.log(data);
    return data.channel;
};


const ChannelEdit = () => {
    const { id } = useParams();

    const [channelPromise] = useState(() => fetchChannel(id));

    const navigate = useNavigate();

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Editing Channel: {id}
                    </Typography>
                    <Button onClick={() => navigate(-1)}> Back</Button>
                </Toolbar>
            </AppBar>
            <ErrorBoundary
                fallback={(error) => (
                    <p>{error.message}</p>
                )}>
                <Suspense fallback={<p> Loading </p>}>
                    <ChannelEditLayout channelPromise={channelPromise} />
                </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default ChannelEdit;