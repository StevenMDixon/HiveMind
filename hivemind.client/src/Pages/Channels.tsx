
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import CardActions from '@mui/material/CardActions';
import Container from '@mui/material/Container';
import Button from '@mui/material/Button';
import Toolbar from '@mui/material/Toolbar';
import AppBar from '@mui/material/AppBar';

import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import TextField from '@mui/material/TextField';

import { useEffect, useState } from 'react';

interface Channel {
    channelID: number;
    channelName: string;
}

const ChannelPage = () => {

    const [createModalState, setCreateModalState] = useState<boolean>(false);
    const handleOpen = () => setCreateModalState(true);
    const handleClose = () => setCreateModalState(false);

    const [channels, setChannels] = useState<Channel[]>([]);

    const [channelName, setChannelName] = useState<string>('');

    const createChannel = async () => {
        await fetch('/channels', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ChannelName: channelName }),
        });
        setChannelName('');
        handleClose();
        fetchChannels();
    };

    const deleteChannel = async (channelId: number) => {
        await fetch(`/channels/${channelId}`, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        });

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
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Channels
                    </Typography>
                    <Button onClick={handleOpen}>Add Channel</Button>
                </Toolbar>
            </AppBar>
            {channels.length > 0 ? channels.map(channel => (
                <Card key={channel.channelID} sx={{ m: 2 }}>
                    <CardContent>
                        <Typography gutterBottom sx={{ color: 'text.primary', fontSize: 14 }}>
                            {channel.channelID}
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
            )) : <p>Loading</p>}
            <Dialog onClose={handleClose} open={createModalState}>
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
                </DialogContent>
                <DialogActions>
                    <Button onClick={createChannel}>Create</Button>
                </DialogActions>
            </Dialog>
            
        </Container>
    )
}

export default ChannelPage;