import { useEffect, useState } from 'react';
import { List, ListItem, ListItemText, CircularProgress, Box } from '@mui/material';
import { SeoRequestDto, getSearchHistory } from '../services/Api';

interface SearchHistoryListProps {
  onSelect: (searchKeyword: string, websiteUrl: string) => void;
  refreshTrigger: any;
}


export default function DistinctSearchHistoryList({ onSelect, refreshTrigger }: SearchHistoryListProps) {
  const [searchHistory, setSearchHistory] = useState<SeoRequestDto[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchSearchHistory = async () => {
      try {
        const data = await getSearchHistory();
        setSearchHistory(data);
        setLoading(false);
      } catch (error) {
        console.error('Failed to fetch search history', error);
        setLoading(false);
      }
    };
    fetchSearchHistory();
  }, [refreshTrigger]);

  const handleClick = (searchKeyword: string, websiteUrl: string) => {
    onSelect(searchKeyword, websiteUrl);
  };

  return (
    <Box sx={{ width: '250px', maxHeight: '500px', overflow: 'auto', border: '1px solid #ddd' }}>
      {loading ? (
        <Box sx={{ display: 'flex', justifyContent: 'center', padding: 2 }}>
          <CircularProgress />
        </Box>
      ) : (
        <List>
          {searchHistory.map((history) => (
            <ListItem button key={history.searchKeyword} onClick={() => handleClick(history.searchKeyword, history.websiteUrl)}>
              <ListItemText primary={history.searchKeyword} secondary={history.websiteUrl} />
            </ListItem>
          ))}
        </List>
      )}
    </Box>
  );
};