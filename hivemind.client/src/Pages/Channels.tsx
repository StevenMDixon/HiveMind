import Container from '@mui/material/Container';
import Button from '@mui/material/Button';

import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import TextField from '@mui/material/TextField';

import Header from './Components/Header';

import { useState } from 'react';

import { useGlobalNotification } from '../Dashboard/useGlobalNotification';

import CustomTable, { type CellData } from './Components/Table';
import { useNavigate } from "react-router-dom";

interface Channel {
    channelID: number;
    channelName: string;
    channelNumber: number;
}

const fetchChannels = async () => {
    const response = await fetch('/channels');

    if (response.ok) {
        const data = await response.json();
        return data.channels;
    }
};


const ChannelPage = () => {

    const [createModalState, setCreateModalState] = useState<boolean>(false);
    const handleModalOpen = () => setCreateModalState(true);
    const handleModalClose = () => setCreateModalState(false);

    const { showNotification } = useGlobalNotification();

    const channelInputDefault = { channelID: 0, channelName: "", channelNumber: 0 }

    const [channelInputs, setChannelInputs] = useState<Channel>(channelInputDefault)

    const updateChannelInputs = (event: React.ChangeEvent<HTMLInputElement>) => {
        const name = event.target.name;
        const value = event.target.value;
        setChannelInputs(values => ({ ...values, [name]: value }))
    }

    const createChannel = async () => {
        const response = await fetch('/channels', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ChannelName: channelInputs.channelName, ChannelNumber: channelInputs.channelNumber }),
        });

        if (response.ok) {
            setChannelInputs(channelInputDefault);
            showNotification("Channel Created", "success");
        } else {
            showNotification("Failed to create channel", "error");
        }
        
        handleModalClose();
        setChannelPromise(fetchChannels());
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

        setChannelPromise(fetchChannels());
    };

    const [channelPromise, setChannelPromise] = useState(() => fetchChannels());
    const navigate = useNavigate();

    const columns = [
        { key: 'channelID', name: 'ID', align: 'left' },
        { key: 'channelName', name: 'Channel Name', align: 'left' },
        { key: 'channelNumber', name: 'Assigned Channel ID', align: 'left' }
    ] as CellData[];

    const actionColumns = [
        { key: 'a1', name: "Edit", action: (e: Channel) => navigate("/channels/" + e.channelID), icon: "Edit" },
        { key: 'a2', name: "Delete", action: (e: Channel) => deleteChannel(e.channelID), icon: "Delete" }
    ] as CellData[];

    const handleRetry = () => {
        setChannelPromise(fetchChannels());
    };

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Channels">
                <Button onClick={handleModalOpen}>Add Channel</Button>
            </Header>
            <CustomTable dataPromise={channelPromise} handleRetry={handleRetry} columns={columns} actionColumns={actionColumns} />

            <Dialog onClose={handleModalClose} open={createModalState}>
                <DialogTitle>Create Channel</DialogTitle>
                <DialogContent>
                    <TextField
                        required
                        name="channelName"
                        label="Channel Name"
                        value={channelInputs.channelName}
                        onChange={updateChannelInputs}
                        sx={{ m: 1 }}
                    />
                    <TextField
                        required
                        name="channelNumber"
                        label="Channel Number"
                        value={channelInputs.channelNumber}
                        onChange={updateChannelInputs}
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