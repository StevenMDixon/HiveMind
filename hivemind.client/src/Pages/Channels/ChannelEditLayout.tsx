import { Container } from "@mui/material"
import type { Channel } from './types';
import { use } from 'react';
import { useNavigate } from "react-router-dom";


import CustomEditForm, { type CustomFormField } from '../Components/EditCustomForm';

interface ChannelEditLayoutProps {
    channelPromise: Promise<Channel>
}

const ChannelEditLayout = ({ channelPromise }: ChannelEditLayoutProps) => {

    const channel = use(channelPromise);

    const navigate = useNavigate();

    const saveChannel = async (item: Channel) => {
        const result = await fetch('/channels/' + item.channelID, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ChannelName: item.channelName, ChannelNumber: item.channelNumber}),
        });

        if (result.ok) navigate(-1);
    }

    const fields = [
        { name: 'channelName', type: "Text", initialValue: channel.channelName },
        { name: 'channelNumber', type: "Text", initialValue: channel.channelNumber },
    ] as CustomFormField[];

    return (
        <Container sx={{ mt: 5 }}>
            <CustomEditForm title="test" save={saveChannel} initialValue={channel} fields={fields} />
        </Container>
    )
}

export default ChannelEditLayout;