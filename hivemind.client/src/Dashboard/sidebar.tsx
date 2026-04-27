import Drawer from '@mui/material/Drawer';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import VideoLibraryIcon from '@mui/icons-material/VideoLibrary';
//import EventNoteIcon from '@mui/icons-material/EventNote';
import LiveTvIcon from '@mui/icons-material/LiveTv';
import HiveIcon from '@mui/icons-material/Hive';
import { Link, useLocation } from 'react-router-dom';
import MovieIcon from '@mui/icons-material/Movie';
import Typography from '@mui/material/Typography'
import SavedSearchIcon from '@mui/icons-material/SavedSearch';
import DisplaySettingsIcon from '@mui/icons-material/DisplaySettings';
import { type SvgIconComponent } from '@mui/icons-material';
import ExpandLess from '@mui/icons-material/ExpandLess';
import ExpandMore from '@mui/icons-material/ExpandMore';
import Collapse from '@mui/material/Collapse';
import SettingsIcon from '@mui/icons-material/Settings';
import PrecisionManufacturingIcon from '@mui/icons-material/PrecisionManufacturing';
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import AutoAwesomeMosaicIcon from '@mui/icons-material/AutoAwesomeMosaic';
import DevicesIcon from '@mui/icons-material/Devices';
import ViewModuleIcon from '@mui/icons-material/ViewModule';
import ViewTimelineIcon from '@mui/icons-material/ViewTimeline';
import SlideshowIcon from '@mui/icons-material/Slideshow';

import { useState } from 'react';

interface RouteItemProps {
    name: string,
    route: string,
    icon: SvgIconComponent,
    current: boolean,
    folder: string
}

interface FolderItemProps {
    name: string,
    icon: SvgIconComponent,
    open: boolean,
    handleOpen: (name: string) => void;
    items: NavItems[],
    route: string
}

interface NavItems {
    key: string,
    route: string,
    icon: SvgIconComponent,
    folder: string
}

const RouteItem = ({name, route, current, icon: Icon }: RouteItemProps) => {
    return (
        <ListItem disablePadding>
            <ListItemButton component={Link} to={route} key={route} selected={current}>
                <ListItemIcon sx={{ minWidth: 0, mr: 2 }} >
                    {<Icon sx={{ m: 0, p: 0 }} color={current ? 'secondary' : 'inherit'} />}
                </ListItemIcon>
                <ListItemText primary={name} />
            </ListItemButton>
        </ListItem>
    )
}

const isCurrent = (elementRoute: string, currentRoute: string) => {
    return elementRoute != '/' && currentRoute.includes(elementRoute);
}

const FolderItem = ({ name, icon: Icon, open, handleOpen, items, route}: FolderItemProps) => {
    return (
        <>
            <ListItemButton onClick={() => handleOpen(name)}>
                <ListItemIcon sx={{ minWidth: 0, mr: 2 }}> 
                    <Icon />
                </ListItemIcon>
                <ListItemText primary={name} />
                {open ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
            <Collapse in={open} timeout="auto" unmountOnExit>
                <List component="div" disablePadding sx={{ pl: 2 }}>
                    {items && items.map(element => <RouteItem key={element.key} name={element.key} route={element.route} icon={element.icon} current={isCurrent(element.route,route)} folder={element.folder} />)}
                </List>
            </Collapse>
        </>
    )
}

const Sidebar: React.FC = () => {
    const location = useLocation();

    const folders = [
        { name: 'Manage', icon: DisplaySettingsIcon, open: false},
        { name: 'Schedule', icon: CalendarMonthIcon, open: false},
        { name: 'Orchestrate', icon: PrecisionManufacturingIcon, open: false},
        { name: 'Settings', icon: SettingsIcon, open: false}
    ]

    const [folderState, setFolderState] = useState(folders);

    const handleSetFolderOpen = (folderName: string) => {
        let f = [...folderState];

        f = f.map(x => x.name == folderName ? {...x, open: !x.open} : x);

        setFolderState(f);
    }

    const navElements = [
        { key: "Channels", route: "/channels", icon: LiveTvIcon, folder: 'Orchestrate'},
        { key: "Libraries", route: "/libraries", icon: VideoLibraryIcon, folder: 'Manage' },
        { key: "Media", route: "/media", icon: MovieIcon, folder: 'Manage' },
        { key: "Queries", route: "/queries", icon: SavedSearchIcon, folder: 'Manage' },
        { key: "Shows", route: "/shows", icon: SlideshowIcon, folder: 'Manage' },
        { key: "Blocks", route: "/", icon: ViewModuleIcon, folder: 'Manage' },
        { key: "Lineups", route: "/lineups", icon: ViewTimelineIcon, folder: 'Schedule' },
        { key: "P. Strategy", route: "/", icon: AutoAwesomeMosaicIcon, folder: 'Schedule' },
        { key: "Drones", route: "/", icon: DevicesIcon, folder: 'Orchestrate' },
    //    { key: "Events", route: "/", icon: MicrowaveIcon, folder: 'Schedule' },
    ];

    const hasCurrentRoute = (elements : NavItems[]) : boolean => {
        return elements.filter(x => x.route == location.pathname).length > 0;
    }

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
                    <HiveIcon fontSize="large" color="secondary"/>
                    <Typography sx={{ml: 1} }>HiveMind</Typography>
            </Toolbar>
            <Divider />
                <List>
                    {navElements.map(element => (
                        element.folder == '' && <RouteItem key={element.key} name={element.key} route={element.route} icon={element.icon} current={element.route === location.pathname} folder={element.folder} />
                    ))}
                    {folderState.map((folder, index) => {
                         const elements = navElements.filter(x => x.folder == folder.name)

                        return < FolderItem key={index} name={folder.name} icon={folder.icon} open={hasCurrentRoute(elements) || folder.open} handleOpen={handleSetFolderOpen} items={elements} route={location.pathname} />
                    }
                    )}
                </List>
            </Drawer>
        </>
    )
}

export default Sidebar;