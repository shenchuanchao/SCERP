import { fileURLToPath, URL } from 'node:url'
import { VitePWA } from 'vite-plugin-pwa'     //引入PWA插件

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueJsx from '@vitejs/plugin-vue-jsx'
import vueDevTools from 'vite-plugin-vue-devtools'

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),    
    vueJsx(),
    vueDevTools(),
    //配置PWA插件
    VitePWA({
      manifest: {
        name: 'SCERP 系统',
        short_name: 'SCERP',
        description: 'SC ERP App description',
        theme_color: '#ffffff',
        icons: [		//添加图标， 注意路径和图像像素正确
          {
            src: 'scc192x192.png',
            sizes: '192x192',
            type: 'image/png',
          },
          {
            src: 'scc512x512.png',
            sizes: '512x512',
            type: 'image/png',
          },
        ],
        screenshots: [
          {
            "src": "/screenshots/desktop-screenshot.png",
            "sizes": "1280x720",
            "type": "image/png",
            "form_factor": "wide",
            "label": "SCERP 系统桌面界面"
          },
          // {
          //   "src": "/screenshots/mobile-screenshot.png", 
          //   "sizes": "375x667",
          //   "type": "image/png",
          //   "form_factor": "narrow",
          //   "label": "SCERP 系统移动界面"
          // },
          {
            "src": "/screenshots/tablet-screenshot.png",
            "sizes": "768x1024",
            "type": "image/png",
            "form_factor": "narrow",
            "label": "SCERP 系统平板界面"
          }
        ]
      },
      registerType: 'autoUpdate',
      workbox: {
        globPatterns: ['**/*.{js,css,html,ico,png,svg}'],		//缓存相关静态资源
        runtimeCaching: [{		//由于要测试第三方API， 这里配置缓存访问第三方API的资源
          handler: 'CacheFirst',
          urlPattern: /^https:\/\/jsonplaceholder/,		//注意，要修改成要测试的API。请使用正则表达式匹配
          method: 'GET',
          options: {
            cacheName: 'test-external-api',		//创建缓存名称
            expiration: {
              maxEntries: 10,
              maxAgeSeconds: 60 * 60 * 24 * 365 // <== 365 days
            },
            cacheableResponse: {
              statuses: [0, 200]
            }
          }
        }]
      },
      devOptions: {
        enabled: true
      }
    }),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    },
  },
})
