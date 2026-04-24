import { Container } from "@mui/material"
import type { Channel } from '../../Types/Channel';
import { use } from 'react';

import CustomForm from '../Components/CustomForm';
import { fields } from './fields';

import { ApiClient } from '../../Api/ApiClient';
import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';

interface ChannelEditLayoutProps {
    channelPromise: Promise<Channel>
}

const ChannelEditLayout = ({ channelPromise }: ChannelEditLayoutProps) => {

    const channel = use(channelPromise);
    console.log(channel)

    const { showNotification } = useGlobalNotification();

    const handleSaveChannel = async (item: Channel) => {
        const result = await ApiClient.saveChannel(item);

        if (result.ok) {
            showNotification("Channel Edited Successfully", "success")
        } else {
            showNotification("Error Editing Channel", "error")
        }
    }

    return (
        <Container sx={{ mt: 5 }}>
            <CustomForm title={channel.channelName} save={handleSaveChannel} initialValue={channel} fields={fields} />
        </Container>
    )
}

export default ChannelEditLayout;