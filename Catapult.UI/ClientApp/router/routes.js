import Containers from 'components/containers'
import Images from 'components/images'
import HomePage from 'components/home-page'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'images', path: '/images', component: Images, display: 'Images', icon: 'list' },
  { name: 'containers', path: '/containers', component: Containers, display: 'Containers', icon: 'list' }
]
