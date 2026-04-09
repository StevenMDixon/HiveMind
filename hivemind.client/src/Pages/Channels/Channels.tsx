import Container from '@mui/material/Container';
import Header from '../Components/Header';

import { useState } from 'react';

import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';

import CustomTable, { type CellData } from '../Components/Table';
import { useNavigate } from "react-router-dom";

import CustomDialog from '../Components/Dialog';

import type { Channel } from '../../Types/Channel';

import {channelDefault, fields } from './fields'

import { fetchChannels } from '../../Api/Channel'; 

const ChannelPage = () => {

    const { showNotification } = useGlobalNotification();


    const createChannel = async (channel: Channel) => {
        const response = await fetch('/channels', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ChannelName: channel.channelName, ChannelNumber: channel.channelNumber }),
        });

        if (response.ok) {
            showNotification("Channel Created", "success");
        } else {
            showNotification("Failed to create channel", "error");
        }
 
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
        { key: 'channelNumber', name: 'Channel Number', align: 'left' }
    ] as CellData<Channel>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", action: (e: Channel) => navigate("/channels/" + e.channelID), icon: "Edit" },
        { key: 'a2', name: "Delete", action: (e: Channel) => deleteChannel(e.channelID), icon: "Delete" }
    ] as CellData<Channel>[];

    const handleRetry = () => {
        setChannelPromise(fetchChannels());
    };

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Channels">
                <CustomDialog buttonText="Add Channel" title="Create Channel" save={createChannel} initialValue={channelDefault} fields={fields} />
            </Header>
            <CustomTable dataPromise={channelPromise} handleRetry={handleRetry} columns={columns} actionColumns={actionColumns} />
        </Container>
    )
}

export default ChannelPage;