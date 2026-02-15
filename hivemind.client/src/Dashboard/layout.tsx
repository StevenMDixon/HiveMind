import { Outlet } from 'react-router';
import SideBar from './sidebar.tsx'
import Container from '@mui/material/Box';
import { NotificationProvider } from './NotificationProvider.tsx';

const DashboardLayout: React.FC = () => {
    return (
        <NotificationProvider>
            <Container sx={{ display: 'flex' }} >
                <SideBar />
                <Outlet />
            </Container>
        </NotificationProvider>
    );
}

export default DashboardLayout;