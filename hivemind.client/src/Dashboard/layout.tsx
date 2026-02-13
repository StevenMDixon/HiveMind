import { Outlet } from 'react-router';
import SideBar from './sidebar.tsx'
import Container from '@mui/material/Box';


const DashboardLayout: React.FC = () => {
    return (
        <Container sx={{ display: 'flex' }} >
            <SideBar />
            <Outlet />
        </Container>
    );
}

export default DashboardLayout;