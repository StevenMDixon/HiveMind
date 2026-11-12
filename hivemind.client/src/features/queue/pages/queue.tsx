import Container from '@mui/material/Container';
import Header from '@/components/Header';
import Stack from '@mui/material/Stack';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { useQuery,useQueryClient } from '@tanstack/react-query';
import { queueQueryOptions } from '../api/queryOptions';
import { useNavigate } from "react-router-dom";
import { getRandomMediabyType } from '../api/requests';
import LinearProgress from '@mui/material/LinearProgress';

interface QueueCardProps {
    name: string;
    done: number;
    total: number;
}

const QueueCard = ({ name, done, total }: QueueCardProps) => {
    const formatName = (name: string) => name[name.length - 1] == 's' ? name : name + 's'
    const navigate = useNavigate();

    const queryClient = useQueryClient(); // Access the cache client

    const handleNavigate = async (name: string) => {
        try {
            // Fetch data imperatively. This respects caching rules.
            const data = await queryClient.fetchQuery({
            queryKey: ['Media'],
            queryFn: () => getRandomMediabyType(name),
            // Optional: specify staleTime if you want to use cached data instead of forcing a network hit
            staleTime: 5000, 
            });

            navigate('/queue/' + data);
            // You can now safely use the data right here in the handler
        } catch (error) {
            console.error('Fetch failed:', error);
        }
        
    }

    return (
        <Card sx={{ width: '100%', marginY: '1em' }}>
            <CardContent>
                <Stack direction="row" sx={{
                    justifyContent: "space-between",
                    alignItems: "center",
                  }}>
                    <Typography>
                        {formatName(name)} Fully Tagged
                    </Typography>
                    <Typography>
                        {done} / {total}
                    </Typography>
                    <Button onClick={ () => handleNavigate(name) } >Let's Tag 'EM</Button>
                </Stack>
                <LinearProgress
                    sx={{ mt: '1em;' }}
                
                variant="determinate"
                value={done/total}
                    aria-label="Items Completed"
                />
            </CardContent>
          </Card>
    );
}

const QueuePage = () => {
    const { data: queues} = useQuery(queueQueryOptions());


    return (
    <Container disableGutters maxWidth={false}>
            <Header Title="Media Queues" />
            <Container>
                <Stack sx={{minHeight: '100%', alignItems: 'space-around'}} >
                    {
                        queues && queues?.map(q => <QueueCard name={q.name} done={q.met} total={q.total} />)
                    }
            </Stack>
            </Container>
    </Container>
  );
}

export default QueuePage;