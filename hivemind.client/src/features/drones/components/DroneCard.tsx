import Paper from '@mui/material/Paper';
import Divider from '@mui/material/Divider';
import Container from '@mui/material/Container';
import Stack from '@mui/material/Stack';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import Avatar from '@mui/material/Avatar';

import Typography from '@mui/material/Typography';
import EmojiNatureIcon from '@mui/icons-material/EmojiNature';

import List from '@mui/material/List';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import ListItemText from '@mui/material/ListItemText';
import ListItem from '@mui/material/ListItem';

import type { Drone } from '../types';
// import { Box } from '@mui/material';
// import ListItemText from '@mui/material/ListItemText';

interface DroneCardProps {
    drone: Drone,
    remove: (i: number) => void,
    edit: (i: number) => void
}

const DroneCard = ({ drone, remove, edit }: DroneCardProps) => {

    return (
        <Paper>
            <Stack direction="row" justifyContent="space-between" >
                    
                <Typography sx={{ margin: "1em" }}><EmojiNatureIcon sx={{ verticalAlign: "bottom"}} fontSize="large" color="secondary" /> {drone.name}</Typography>
                <Stack direction="row">
                    <IconButton>
                        <EditIcon onClick={() => edit(drone.droneId) } />
                    </IconButton>
                    <IconButton>
                        <DeleteIcon onClick={() => remove(drone.droneId)} />
                    </IconButton>
                </Stack>
            </Stack>
            <Divider/>
            <Container sx={{margin: "1em"}} >
                <Typography> Host Name: {drone.hostName} </Typography>
                <Typography> Stations: </Typography>
                <List>
                    {drone.stations && drone.stations.map(station =>
                        <ListItem>
                            <ListItemAvatar>
                                <Avatar alt="Remy Sharp" src={station.stationLogo} />
                            </ListItemAvatar>
                            <ListItemText>
                                {station.stationName}
                            </ListItemText>
                        </ListItem>
                    )
                    }
                </List>
            </Container>
        </Paper>
    )
}


export default DroneCard;