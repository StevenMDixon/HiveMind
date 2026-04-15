import { Container } from "@mui/material"
import type { Channel } from '../../Types/Channel';
import { use } from 'react';
import { useNavigate } from "react-router-dom";

import CustomForm from '../Components/CustomForm';

import { fields } from './fields';

import { saveChannel } from '../../Api/Channel'; 

interface ChannelEditLayoutProps {
    channelPromise: Promise<Channel>
}

const ChannelEditLayout = ({ channelPromise }: ChannelEditLayoutProps) => {

    const channel = use(channelPromise);

    const navigate = useNavigate();

    const handleSaveChannel = async (item: Channel) => {
        const result = await saveChannel(item);

        if (result.ok) navigate(-1);
    }

    return (
        <Container sx={{ mt: 5 }}>
            <CustomForm title={channel.channelName} save={handleSaveChannel} initialValue={channel} fields={fields} />
        </Container>
    )
}

export default ChannelEditLayout;