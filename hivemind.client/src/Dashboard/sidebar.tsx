import Drawer from '@mui/material/Drawer';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import VideoLibraryIcon from '@mui/icons-material/VideoLibrary';
import EventNoteIcon from '@mui/icons-material/EventNote';
import LibraryBooksIcon from '@mui/icons-material/LibraryBooks';
import TvIcon from '@mui/icons-material/Tv';
import HiveIcon from '@mui/icons-material/Hive';
import { Link, useLocation } from 'react-router-dom';


const Sidebar: React.FC = () => {
    const location = useLocation();

    const navElements = [
        { key: "Channels", route: "/channels", icon: TvIcon },
        { key: "Collections", route: "/collections", icon: LibraryBooksIcon },
        { key: "Libraries", route: "/libraries", icon: VideoLibraryIcon },
        { key: "Schedules", route: "/schedules", icon: EventNoteIcon },
    ];

    return (
        <>
            <Drawer
                sx={{
                    width: 200,
                    flexShrink: 0,
                    '& .MuiDrawer-paper': {
                        width: 200,
                        boxSizing: 'border-box',
                    },
                }}
                variant="permanent"
                anchor="left"
            >
                <Toolbar>
                <HiveIcon />
                <p>HiveMind</p>
            </Toolbar>
            <Divider />
                <List>
                    { navElements.map( element => (
                        <ListItem key={element.key} disablePadding>
                            <ListItemButton component={Link} to={element.route} key={element.route} selected={ element.route == location.pathname}>
                            <ListItemIcon>
                                    {<element.icon />}
                            </ListItemIcon>
                            <ListItemText primary={element.key} />
                        </ListItemButton>
                    </ListItem>
                    ))}
            </List>
            </Drawer>
        </>
    )
}

export default Sidebar;