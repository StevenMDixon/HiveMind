import { Outlet } from 'react-router';
import Header from './header.tsx';
import SideBar from './sidebar.tsx'
import Box from '@mui/material/Box';



const DashboardLayout: React.FC = () => {
    return (
        <Box sx={{ display: 'flex' }}>
            <SideBar />
            <Outlet />
        </Box>
    );
}

export default DashboardLayout;