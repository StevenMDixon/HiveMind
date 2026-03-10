
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import CardActions from '@mui/material/CardActions';
import Container from '@mui/material/Container';
import Button from '@mui/material/Button';

import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import TextField from '@mui/material/TextField';

import Header from './Components/Header';

import RoundedLoadingFiller from './Components/RoundedLoadingFiller';

import { useEffect, useState } from 'react';

import { useGlobalNotification } from '../Dashboard/useGlobalNotification';

interface Channel {
    channelID: number;
    channelName: string;
    channelNumber: number;
}

const ChannelPage = () => {

    const [createModalState, setCreateModalState] = useState<boolean>(false);
    const handleModalOpen = () => setCreateModalState(true);
    const handleModalClose = () => setCreateModalState(false);

    const { showNotification } = useGlobalNotification();

    const [channels, setChannels] = useState<Channel[]>([]);

    const [channelName, setChannelName] = useState<string>('');
    const [channelNumber, setChannelNumber] = useState<number>(0);

    const createChannel = async () => {
        const response = await fetch('/channels', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ChannelName: channelName, ChannelNumber: channelNumber }),
        });

        if (response.ok) {
            setChannelName('');
            showNotification("Channel Created", "success");
        } else {
            showNotification("Failed to create channel", "error");
        }
        
        handleModalClose();
        fetchChannels();
    };

    const deleteChannel = async (channelId: number) => {
        const response =  await fetch(`/channels/${channelId}`, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        });

        if (response.ok) {
            showNotification("Channel deleted", "success")
        } else {
            showNotification("Failed to delete channel", "error");
        }

        fetchChannels();
    };

    const fetchChannels = async () => {
        const response = await fetch('/channels');

        if (response.ok) {
            const data = await response.json();
            setChannels(data.channels);
        }
    };

    useEffect(() => {
        fetchChannels();
    }, []);

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Channels">
                <Button onClick={handleModalOpen}>Add Channel</Button>
            </Header>
            {channels.length > 0 ? channels.map(channel => (
                <Card key={channel.channelID} sx={{ m: 2 }}>
                    <CardContent>
                        <Typography gutterBottom sx={{ color: 'text.primary', fontSize: 14 }}>
                            {channel.channelID}
                        </Typography>
                        <Typography gutterBottom sx={{ color: 'text.primary', fontSize: 14 }}>
                            {channel.channelNumber}
                        </Typography>
                        <Typography gutterBottom sx={{ color: 'text.secondary', fontSize: 14 }}>
                            {channel.channelName}
                        </Typography>
                    </CardContent>
                    <CardActions sx={{ justifyContent: 'flex-end' }}>
                        <Button size="small" color="primary" onClick={() => deleteChannel(channel.channelID)}>
                            Delete
                        </Button>
                        <Button size="small" color="primary">
                            Edit
                        </Button>
                    </CardActions>
                </Card>
            )) : <RoundedLoadingFiller size={7} />
            }
            <Dialog onClose={handleModalClose} open={createModalState}>
                <DialogTitle>Create Channel</DialogTitle>
                <DialogContent>
                    <TextField
                        required
                        id="outlined-required"
                        label="Channel Name"
                        value={channelName}
                        onChange={(event: React.ChangeEvent<HTMLInputElement>) => setChannelName(event.target.value)}
                        sx={{ m: 1 }}
                    />
                    <TextField
                        required
                        id="outlined-required"
                        label="Channel Number"
                        value={channelNumber}
                        onChange={(event: React.ChangeEvent<HTMLInputElement>) => setChannelNumber(Number(event.target.value))}
                        sx={{ m: 1 }}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={createChannel}>Create</Button>
                </DialogActions>
            </Dialog>
        </Container>
    )
}

export default ChannelPage;