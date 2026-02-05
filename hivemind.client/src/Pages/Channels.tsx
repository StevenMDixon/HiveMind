import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import CardActions from '@mui/material/CardActions';
import Container from '@mui/material/Container';
import Paper from '@mui/material/Paper';
import { AppBar } from '@mui/material';
import Button from '@mui/material/Button';
import { useEffect, useState } from 'react';

interface Channel {
    channelID: number;
    channelName: string;
}

const ChannelPage = () => {

    const [channels, setChannels] = useState<Channel[]>();


    async function ChannelQuery() {
        const response = await fetch('/channels');

        if (response.ok) {
            const data = await response.json();
            setChannels(data.channels);
        }
    }

    useEffect(() => {
        ChannelQuery();
    }, []);

    return (
        <Container>
            {/*<AppBar color="inherit">*/}
            {/*    <ToolBar>*/}
            {/*        <p>Wooo </p>*/}
            {/*    </ToolBar>*/}
            {/*</AppBar>*/}
            {/*<Paper elevation={1}>*/}
            {channels ? channels.map(channel => (
                <Card sx={{ minWidth: 600, m: 2}}>
                    <CardContent>
                        <Typography gutterBottom sx={{ color: 'text.primary', fontSize: 14 }}>
                            {channel.channelID}
                        </Typography>
                        <Typography gutterBottom sx={{ color: 'text.secondary', fontSize: 14 }}>
                            {channel.channelName}
                        </Typography>


                    </CardContent>
                    <CardActions sx={{ justifyContent: 'flex-end' }}>
                        <Button size="small" color="primary">
                            Delete
                        </Button>
                        <Button size="small" color="primary">
                            Edit
                        </Button>
                    </CardActions>
                </Card>
            )) : <p>Loading</p>}
            {/*</Paper>*/}
        </Container>
    )
}

export default ChannelPage;