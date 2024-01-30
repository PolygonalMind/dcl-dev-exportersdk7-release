import { engine, Transform, GltfContainer, pointerEventsSystem, InputAction } from "@dcl/sdk/ecs"



// Entity: s0_Main_Camera_01 //
const s0_Main_Camera_01 = engine.addEntity()
Transform.create(s0_Main_Camera_01, {
	position: {x:0,y:1,z:-10},
	rotation: {x:0,y:0,z:0,w:1},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Directional_Light_01 //
const s0_Directional_Light_01 = engine.addEntity()
Transform.create(s0_Directional_Light_01, {
	position: {x:0,y:3,z:0},
	rotation: {x:0.4082179,y:-0.2345697,z:0.1093816,w:0.8754261},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Visuals_01 //
const s0_Visuals_01 = engine.addEntity()
Transform.create(s0_Visuals_01, {
	position: {x:0,y:0,z:0},
	rotation: {x:0,y:0,z:0,w:1},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Floor_01 //
const s0_Floor_01 = engine.addEntity()
GltfContainer.create(s0_Floor_01, {
	src: "unity_assets/s0_Floor_01.glb",
})
Transform.create(s0_Floor_01, {
	position: {x:8,y:0,z:8},
	rotation: {x:-3.090862E-08,y:0.7071068,z:0.7071069,w:-3.090862E-08},
	scale: {x:16,y:16,z:1},
	parent: s0_Visuals_01
})


// Entity: s0_Floor__3__01 //
const s0_Floor__3__01 = engine.addEntity()
GltfContainer.create(s0_Floor__3__01, {
	src: "unity_assets/s0_Floor__3__01.glb",
})
Transform.create(s0_Floor__3__01, {
	position: {x:8,y:-1.907349E-06,z:24},
	rotation: {x:-3.090862E-08,y:0.7071068,z:0.7071069,w:-3.090862E-08},
	scale: {x:16,y:16,z:1},
	parent: s0_Visuals_01
})


// Entity: s0_Floor__6__01 //
const s0_Floor__6__01 = engine.addEntity()
GltfContainer.create(s0_Floor__6__01, {
	src: "unity_assets/s0_Floor__6__01.glb",
})
Transform.create(s0_Floor__6__01, {
	position: {x:8,y:-3.814697E-06,z:40},
	rotation: {x:-3.090862E-08,y:0.7071068,z:0.7071069,w:-3.090862E-08},
	scale: {x:16,y:16,z:1},
	parent: s0_Visuals_01
})


// Entity: s0_Floor__9__01 //
const s0_Floor__9__01 = engine.addEntity()
GltfContainer.create(s0_Floor__9__01, {
	src: "unity_assets/s0_Floor__9__01.glb",
})
Transform.create(s0_Floor__9__01, {
	position: {x:8,y:-5.722046E-06,z:56},
	rotation: {x:-3.090862E-08,y:0.7071068,z:0.7071069,w:-3.090862E-08},
	scale: {x:16,y:16,z:1},
	parent: s0_Visuals_01
})


// Entity: s0_Floor__1__01 //
const s0_Floor__1__01 = engine.addEntity()
GltfContainer.create(s0_Floor__1__01, {
	src: "unity_assets/s0_Floor__1__01.glb",
})
Transform.create(s0_Floor__1__01, {
	position: {x:24,y:0,z:8},
	rotation: {x:-3.090862E-08,y:0.7071068,z:0.7071069,w:-3.090862E-08},
	scale: {x:16,y:16,z:1},
	parent: s0_Visuals_01
})


// Entity: s0_Floor__4__01 //
const s0_Floor__4__01 = engine.addEntity()
GltfContainer.create(s0_Floor__4__01, {
	src: "unity_assets/s0_Floor__4__01.glb",
})
Transform.create(s0_Floor__4__01, {
	position: {x:24,y:-1.907349E-06,z:24},
	rotation: {x:-3.090862E-08,y:0.7071068,z:0.7071069,w:-3.090862E-08},
	scale: {x:16,y:16,z:1},
	parent: s0_Visuals_01
})


// Entity: s0_Floor__7__01 //
const s0_Floor__7__01 = engine.addEntity()
GltfContainer.create(s0_Floor__7__01, {
	src: "unity_assets/s0_Floor__7__01.glb",
})
Transform.create(s0_Floor__7__01, {
	position: {x:24,y:-3.814697E-06,z:40},
	rotation: {x:-3.090862E-08,y:0.7071068,z:0.7071069,w:-3.090862E-08},
	scale: {x:16,y:16,z:1},
	parent: s0_Visuals_01
})


// Entity: s0_Floor__10__01 //
const s0_Floor__10__01 = engine.addEntity()
GltfContainer.create(s0_Floor__10__01, {
	src: "unity_assets/s0_Floor__10__01.glb",
})
Transform.create(s0_Floor__10__01, {
	position: {x:24,y:-5.722046E-06,z:56},
	rotation: {x:-3.090862E-08,y:0.7071068,z:0.7071069,w:-3.090862E-08},
	scale: {x:16,y:16,z:1},
	parent: s0_Visuals_01
})


// Entity: s0_Floor__2__01 //
const s0_Floor__2__01 = engine.addEntity()
GltfContainer.create(s0_Floor__2__01, {
	src: "unity_assets/s0_Floor__2__01.glb",
})
Transform.create(s0_Floor__2__01, {
	position: {x:40,y:0,z:8},
	rotation: {x:-3.090862E-08,y:0.7071068,z:0.7071069,w:-3.090862E-08},
	scale: {x:16,y:16,z:1},
	parent: s0_Visuals_01
})


// Entity: s0_Floor__5__01 //
const s0_Floor__5__01 = engine.addEntity()
GltfContainer.create(s0_Floor__5__01, {
	src: "unity_assets/s0_Floor__5__01.glb",
})
Transform.create(s0_Floor__5__01, {
	position: {x:40,y:-1.907349E-06,z:24},
	rotation: {x:-3.090862E-08,y:0.7071068,z:0.7071069,w:-3.090862E-08},
	scale: {x:16,y:16,z:1},
	parent: s0_Visuals_01
})


// Entity: s0_Floor__8__01 //
const s0_Floor__8__01 = engine.addEntity()
GltfContainer.create(s0_Floor__8__01, {
	src: "unity_assets/s0_Floor__8__01.glb",
})
Transform.create(s0_Floor__8__01, {
	position: {x:40,y:-3.814697E-06,z:40},
	rotation: {x:-3.090862E-08,y:0.7071068,z:0.7071069,w:-3.090862E-08},
	scale: {x:16,y:16,z:1},
	parent: s0_Visuals_01
})


// Entity: s0_Str_Ruins_07_Art_01 //
const s0_Str_Ruins_07_Art_01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_07_Art_01, {
	src: "unity_assets/s0_Str_Ruins_07_Art_01.glb",
})
Transform.create(s0_Str_Ruins_07_Art_01, {
	position: {x:19.4342,y:0,z:16.5808},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1.0358,y:1,z:1.0195}
})


// Entity: s0_Str_Ruins_07_Art__1__01 //
const s0_Str_Ruins_07_Art__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_07_Art__1__01, {
	src: "unity_assets/s0_Str_Ruins_07_Art_01.glb",
})
Transform.create(s0_Str_Ruins_07_Art__1__01, {
	position: {x:20.7875,y:-3.814697E-06,z:15.0528},
	rotation: {x:0,y:0.9238797,z:0,w:-0.3826832},
	scale: {x:0.9275144,y:1,z:0.6789}
})


// Entity: s0_Str_Ruins_07_Art__2__01 //
const s0_Str_Ruins_07_Art__2__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_07_Art__2__01, {
	src: "unity_assets/s0_Str_Ruins_07_Art_01.glb",
})
Transform.create(s0_Str_Ruins_07_Art__2__01, {
	position: {x:23.1426,y:0,z:16.3386},
	rotation: {x:0,y:0.7871476,z:0,w:-0.6167647},
	scale: {x:1.0812,y:1,z:1.0716}
})


// Entity: s0_Str_Dock_01_Art_01 //
const s0_Str_Dock_01_Art_01 = engine.addEntity()
GltfContainer.create(s0_Str_Dock_01_Art_01, {
	src: "unity_assets/s0_Str_Dock_01_Art_01.glb",
})
Transform.create(s0_Str_Dock_01_Art_01, {
	position: {x:31.4,y:0.02,z:16.282},
	rotation: {x:-0.0228275,y:0.9997394,z:-9.978217E-10,w:-4.37E-08},
	scale: {x:1.3094,y:1.3094,z:1.3094}
})


// Entity: s0_Water_Pond_01__1__01 //
const s0_Water_Pond_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Water_Pond_01__1__01, {
	src: "unity_assets/s0_Water_Pond_01__1__01.glb",
})
Transform.create(s0_Water_Pond_01__1__01, {
	position: {x:40.03252,y:0.06,z:15.35327},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Water_Pond_01__2__01 //
const s0_Water_Pond_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Water_Pond_01__2__01, {
	src: "unity_assets/s0_Water_Pond_01__1__01.glb",
})
Transform.create(s0_Water_Pond_01__2__01, {
	position: {x:39.4286,y:0.07,z:24.0615},
	rotation: {x:0,y:0.9238797,z:0,w:0.3826832},
	scale: {x:0.9203,y:1,z:0.9243}
})


// Entity: s0_Water_Pond_01__6__01 //
const s0_Water_Pond_01__6__01 = engine.addEntity()
GltfContainer.create(s0_Water_Pond_01__6__01, {
	src: "unity_assets/s0_Water_Pond_01__1__01.glb",
})
Transform.create(s0_Water_Pond_01__6__01, {
	position: {x:39.7241,y:0.07,z:8.7695},
	rotation: {x:0,y:0.9238797,z:0,w:0.3826832},
	scale: {x:0.8878,y:1,z:0.9063}
})


// Entity: s0_Water_Pond_01__3__01 //
const s0_Water_Pond_01__3__01 = engine.addEntity()
GltfContainer.create(s0_Water_Pond_01__3__01, {
	src: "unity_assets/s0_Water_Pond_01__1__01.glb",
})
Transform.create(s0_Water_Pond_01__3__01, {
	position: {x:39.65,y:0.11,z:33.12},
	rotation: {x:0,y:1,z:0,w:-9.079786E-07},
	scale: {x:0.74367,y:0.74367,z:0.74367}
})


// Entity: s0_Water_Pond_01__4__01 //
const s0_Water_Pond_01__4__01 = engine.addEntity()
GltfContainer.create(s0_Water_Pond_01__4__01, {
	src: "unity_assets/s0_Water_Pond_01__1__01.glb",
})
Transform.create(s0_Water_Pond_01__4__01, {
	position: {x:41.8,y:0.18,z:38.87},
	rotation: {x:0,y:1,z:0,w:-9.079786E-07},
	scale: {x:0.74367,y:0.74367,z:0.74367}
})


// Entity: s0_Water_Pond_01__5__01 //
const s0_Water_Pond_01__5__01 = engine.addEntity()
GltfContainer.create(s0_Water_Pond_01__5__01, {
	src: "unity_assets/s0_Water_Pond_01__1__01.glb",
})
Transform.create(s0_Water_Pond_01__5__01, {
	position: {x:39.47,y:0.02,z:42.464},
	rotation: {x:0,y:1,z:0,w:-9.079786E-07},
	scale: {x:0.60563,y:0.60563,z:0.60563}
})


// Entity: s0_Water_Pond_01__7__01 //
const s0_Water_Pond_01__7__01 = engine.addEntity()
GltfContainer.create(s0_Water_Pond_01__7__01, {
	src: "unity_assets/s0_Water_Pond_01__1__01.glb",
})
Transform.create(s0_Water_Pond_01__7__01, {
	position: {x:43.26,y:0.02,z:3.96},
	rotation: {x:0,y:1,z:0,w:-9.079786E-07},
	scale: {x:0.60563,y:0.60563,z:0.60563}
})


// Entity: s0_AG_Stand_Info_Leaderboards_Art_01 //
const s0_AG_Stand_Info_Leaderboards_Art_01 = engine.addEntity()
GltfContainer.create(s0_AG_Stand_Info_Leaderboards_Art_01, {
	src: "unity_assets/s0_AG_Stand_Info_Leaderboards_Art_01.glb",
})
Transform.create(s0_AG_Stand_Info_Leaderboards_Art_01, {
	position: {x:19.22,y:-1.25,z:38.44},
	rotation: {x:0,y:0.6087616,z:0,w:-0.7933532},
	scale: {x:0.65528,y:0.65528,z:0.65528}
})


// Entity: s0_Prop_Smartphone_Art_01 //
const s0_Prop_Smartphone_Art_01 = engine.addEntity()
GltfContainer.create(s0_Prop_Smartphone_Art_01, {
	src: "unity_assets/s0_Prop_Smartphone_Art_01.glb",
})
Transform.create(s0_Prop_Smartphone_Art_01, {
	position: {x:14.929,y:0.06400001,z:56.815},
	rotation: {x:-0.1064175,y:0.5740533,z:-0.07557308,w:-0.8083481},
	scale: {x:1.9723,y:1.9723,z:1.9723}
})


// Entity: s0_Prop_Button_02_Art_01 //
const s0_Prop_Button_02_Art_01 = engine.addEntity()
GltfContainer.create(s0_Prop_Button_02_Art_01, {
	src: "unity_assets/s0_Prop_Button_02_Art_01.glb",
})
Transform.create(s0_Prop_Button_02_Art_01, {
	position: {x:15.99427,y:-1.907349E-05,z:55.01377},
	rotation: {x:0,y:0.8879969,z:0,w:0.4598496},
	scale: {x:44.11564,y:100,z:44.11564}
})
pointerEventsSystem.onPointerDown(
	{entity: s0_Prop_Button_02_Art_01, opts: {button: InputAction.IA_ANY, maxDistance: 8, hoverText:"Stop Animation"}},
	function(cmd){
		if(cmd.button === InputAction.IA_POINTER){
			Animator.stopAllAnimations(s0_Prop_Smartphone_Art_01)
		}
	}
)


// Entity: s0_Prop_Button_02_Art__1__01 //
const s0_Prop_Button_02_Art__1__01 = engine.addEntity()
GltfContainer.create(s0_Prop_Button_02_Art__1__01, {
	src: "unity_assets/s0_Prop_Button_02_Art__1__01.glb",
})
Transform.create(s0_Prop_Button_02_Art__1__01, {
	position: {x:16.966,y:-1.907349E-05,z:56.389},
	rotation: {x:0,y:0.8879969,z:0,w:0.4598496},
	scale: {x:44.11564,y:100,z:44.11564}
})
pointerEventsSystem.onPointerDown(
	{entity: s0_Prop_Button_02_Art__1__01, opts: {button: InputAction.IA_ANY, maxDistance: 8, hoverText:"Play"}},
	function(cmd){
		if(cmd.button === InputAction.IA_POINTER){
			Animator.playSingleAnimation(s0_Prop_Smartphone_Art_01, "Line_Stikers_Anim", true)
		}
	}
)


// Entity: s0_Prop_Button_02_Art__2__01 //
const s0_Prop_Button_02_Art__2__01 = engine.addEntity()
GltfContainer.create(s0_Prop_Button_02_Art__2__01, {
	src: "unity_assets/s0_Prop_Button_02_Art__2__01.glb",
})
Transform.create(s0_Prop_Button_02_Art__2__01, {
	position: {x:17.953,y:-1.907349E-05,z:57.813},
	rotation: {x:0,y:0.8879969,z:0,w:0.4598496},
	scale: {x:44.11564,y:100,z:44.11564}
})
pointerEventsSystem.onPointerDown(
	{entity: s0_Prop_Button_02_Art__2__01, opts: {button: InputAction.IA_ANY, maxDistance: 8, hoverText:"Play"}},
	function(cmd){
		if(cmd.button === InputAction.IA_POINTER){
			Animator.playSingleAnimation(s0_Prop_Smartphone_Art_01, "Line_Stikers_Anim", true)
		}
	}
)


// Entity: s0_Str_Portal_02_Art_01 //
const s0_Str_Portal_02_Art_01 = engine.addEntity()
GltfContainer.create(s0_Str_Portal_02_Art_01, {
	src: "unity_assets/s0_Str_Portal_02_Art_01.glb",
})
Transform.create(s0_Str_Portal_02_Art_01, {
	position: {x:6.8,y:-1.239777E-05,z:32.62},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:0.452,y:0.452,z:0.452}
})


// Entity: s0_Str_Ruins_01_Art_01 //
const s0_Str_Ruins_01_Art_01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_01_Art_01, {
	src: "unity_assets/s0_Str_Ruins_01_Art_01.glb",
})
Transform.create(s0_Str_Ruins_01_Art_01, {
	position: {x:6.1,y:-4.44,z:37.27},
	rotation: {x:0,y:0.5000017,z:0,w:-0.8660244},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Str_Ruins_02_Art_01 //
const s0_Str_Ruins_02_Art_01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_02_Art_01, {
	src: "unity_assets/s0_Str_Ruins_02_Art_01.glb",
})
Transform.create(s0_Str_Ruins_02_Art_01, {
	position: {x:2.62,y:-4.5417,z:28.6},
	rotation: {x:0,y:0.6087628,z:0,w:-0.7933523},
	scale: {x:1.1326,y:1.1326,z:1.1326}
})


// Entity: s0_Str_Ruins_02_Art__1__01 //
const s0_Str_Ruins_02_Art__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_02_Art__1__01, {
	src: "unity_assets/s0_Str_Ruins_02_Art_01.glb",
})
Transform.create(s0_Str_Ruins_02_Art__1__01, {
	position: {x:4.13,y:-6.66,z:29.8},
	rotation: {x:0,y:0.8660259,z:0,w:0.4999992},
	scale: {x:1.1326,y:1.1326,z:1.1326}
})


// Entity: s0_Str_Ruins_02_Art__2__01 //
const s0_Str_Ruins_02_Art__2__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_02_Art__2__01, {
	src: "unity_assets/s0_Str_Ruins_02_Art_01.glb",
})
Transform.create(s0_Str_Ruins_02_Art__2__01, {
	position: {x:4.327343,y:-6.647874,z:16.01852},
	rotation: {x:-0.02948975,y:0.6543244,z:-0.02782593,w:0.7551264},
	scale: {x:1.1326,y:1.1326,z:1.1326}
})


// Entity: s0_Str_Ruins_04_Art_01 //
const s0_Str_Ruins_04_Art_01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_04_Art_01, {
	src: "unity_assets/s0_Str_Ruins_04_Art_01.glb",
})
Transform.create(s0_Str_Ruins_04_Art_01, {
	position: {x:8.28,y:-2.21,z:4.59},
	rotation: {x:0,y:0.9930851,z:0,w:-0.1173964},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Str_Ruins_04_Art__1__01 //
const s0_Str_Ruins_04_Art__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_04_Art__1__01, {
	src: "unity_assets/s0_Str_Ruins_04_Art_01.glb",
})
Transform.create(s0_Str_Ruins_04_Art__1__01, {
	position: {x:5.28844,y:-2.21,z:7.524187},
	rotation: {x:0,y:0.9834331,z:0,w:-0.1812718},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Str_Ruins_04_Art__2__01 //
const s0_Str_Ruins_04_Art__2__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_04_Art__2__01, {
	src: "unity_assets/s0_Str_Ruins_04_Art_01.glb",
})
Transform.create(s0_Str_Ruins_04_Art__2__01, {
	position: {x:3.81,y:-2.21,z:12.08},
	rotation: {x:0,y:0.9530983,z:0,w:-0.3026609},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Str_Ruins_08_Art_01 //
const s0_Str_Ruins_08_Art_01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_08_Art_01, {
	src: "unity_assets/s0_Str_Ruins_08_Art_01.glb",
})
Transform.create(s0_Str_Ruins_08_Art_01, {
	position: {x:24.51,y:-7.629395E-06,z:11.25},
	rotation: {x:0,y:0.7933542,z:0,w:-0.6087605},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Str_Ruins_09_Art_01 //
const s0_Str_Ruins_09_Art_01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_09_Art_01, {
	src: "unity_assets/s0_Str_Ruins_09_Art_01.glb",
})
Transform.create(s0_Str_Ruins_09_Art_01, {
	position: {x:27.31,y:-5.722046E-06,z:22.8},
	rotation: {x:0,y:0.7071078,z:0,w:0.7071057},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Str_Sign_Dir_Art_01 //
const s0_Str_Sign_Dir_Art_01 = engine.addEntity()
GltfContainer.create(s0_Str_Sign_Dir_Art_01, {
	src: "unity_assets/s0_Str_Sign_Dir_Art_01.glb",
})
Transform.create(s0_Str_Sign_Dir_Art_01, {
	position: {x:30.34,y:-1.907349E-06,z:11.76},
	rotation: {x:0,y:0.8660259,z:0,w:-0.4999992},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Teddy_Art_01 //
const s0_Teddy_Art_01 = engine.addEntity()
GltfContainer.create(s0_Teddy_Art_01, {
	src: "unity_assets/s0_Teddy_Art_01.glb",
})
Transform.create(s0_Teddy_Art_01, {
	position: {x:22.41,y:-7.152557E-06,z:27.43},
	rotation: {x:0,y:0.7523611,z:0,w:0.6587509},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Terrain_Rock_1_Art_01 //
const s0_Terrain_Rock_1_Art_01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Rock_1_Art_01, {
	src: "unity_assets/s0_Terrain_Rock_1_Art_01.glb",
})
Transform.create(s0_Terrain_Rock_1_Art_01, {
	position: {x:17.78,y:-1.35,z:18.52},
	rotation: {x:0,y:0.9238798,z:0,w:-0.3826828},
	scale: {x:4.3411,y:4.3411,z:4.3411}
})


// Entity: s0_Terrain_Rock_1_Art__1__01 //
const s0_Terrain_Rock_1_Art__1__01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Rock_1_Art__1__01, {
	src: "unity_assets/s0_Terrain_Rock_1_Art_01.glb",
})
Transform.create(s0_Terrain_Rock_1_Art__1__01, {
	position: {x:17.2,y:-1.35,z:21.16},
	rotation: {x:0,y:0.7630152,z:0,w:-0.6463806},
	scale: {x:6.33627,y:6.33627,z:6.33627}
})


// Entity: s0_Str_Bridge_01_Art_01 //
const s0_Str_Bridge_01_Art_01 = engine.addEntity()
GltfContainer.create(s0_Str_Bridge_01_Art_01, {
	src: "unity_assets/s0_Str_Bridge_01_Art_01.glb",
})
Transform.create(s0_Str_Bridge_01_Art_01, {
	position: {x:23.196,y:-0.57,z:41.515},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:0.44315,y:0.44315,z:0.44315}
})


// Entity: s0_Veg_Bush_01_a_Art_01 //
const s0_Veg_Bush_01_a_Art_01 = engine.addEntity()
GltfContainer.create(s0_Veg_Bush_01_a_Art_01, {
	src: "unity_assets/s0_Veg_Bush_01_a_Art_01.glb",
})
Transform.create(s0_Veg_Bush_01_a_Art_01, {
	position: {x:27.21,y:-9.536743E-06,z:42.97},
	rotation: {x:0,y:0.8101344,z:0,w:-0.5862444},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Veg_Bush_01_a_Art__1__01 //
const s0_Veg_Bush_01_a_Art__1__01 = engine.addEntity()
GltfContainer.create(s0_Veg_Bush_01_a_Art__1__01, {
	src: "unity_assets/s0_Veg_Bush_01_a_Art_01.glb",
})
Transform.create(s0_Veg_Bush_01_a_Art__1__01, {
	position: {x:26.72277,y:-9.536743E-06,z:37.16868},
	rotation: {x:0,y:0.5236483,z:0,w:-0.8519346},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Veg_Flower_05_Art_01 //
const s0_Veg_Flower_05_Art_01 = engine.addEntity()
GltfContainer.create(s0_Veg_Flower_05_Art_01, {
	src: "unity_assets/s0_Veg_Flower_05_Art_01.glb",
})
Transform.create(s0_Veg_Flower_05_Art_01, {
	position: {x:28.77341,y:-1.049042E-05,z:38.69902},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Veg_Flower_05_Art__1__01 //
const s0_Veg_Flower_05_Art__1__01 = engine.addEntity()
GltfContainer.create(s0_Veg_Flower_05_Art__1__01, {
	src: "unity_assets/s0_Veg_Flower_05_Art_01.glb",
})
Transform.create(s0_Veg_Flower_05_Art__1__01, {
	position: {x:29.06,y:-0.0033243,z:39.3},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1.1768,y:1.1768,z:1.1768}
})


// Entity: s0_Veg_Flower_05_Art__2__01 //
const s0_Veg_Flower_05_Art__2__01 = engine.addEntity()
GltfContainer.create(s0_Veg_Flower_05_Art__2__01, {
	src: "unity_assets/s0_Veg_Flower_05_Art_01.glb",
})
Transform.create(s0_Veg_Flower_05_Art__2__01, {
	position: {x:28.659,y:-0.0013745,z:43.72},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1.072783,y:1.072783,z:1.072783}
})


// Entity: s0_Veg_Bush_01_d_Art_01 //
const s0_Veg_Bush_01_d_Art_01 = engine.addEntity()
GltfContainer.create(s0_Veg_Bush_01_d_Art_01, {
	src: "unity_assets/s0_Veg_Bush_01_d_Art_01.glb",
})
Transform.create(s0_Veg_Bush_01_d_Art_01, {
	position: {x:19.79,y:-1.335144E-05,z:42.55},
	rotation: {x:0,y:0.9645457,z:0,w:0.2639163},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Veg_Bush_01_d_Art__1__01 //
const s0_Veg_Bush_01_d_Art__1__01 = engine.addEntity()
GltfContainer.create(s0_Veg_Bush_01_d_Art__1__01, {
	src: "unity_assets/s0_Veg_Bush_01_d_Art_01.glb",
})
Transform.create(s0_Veg_Bush_01_d_Art__1__01, {
	position: {x:20.007,y:-0.34362,z:40.455},
	rotation: {x:0,y:0.8250921,z:0,w:0.5649981},
	scale: {x:1.3536,y:1.3536,z:1.3536}
})


// Entity: s0_Veg_Bush_02_b_Art_01 //
const s0_Veg_Bush_02_b_Art_01 = engine.addEntity()
GltfContainer.create(s0_Veg_Bush_02_b_Art_01, {
	src: "unity_assets/s0_Veg_Bush_02_b_Art_01.glb",
})
Transform.create(s0_Veg_Bush_02_b_Art_01, {
	position: {x:18.815,y:-0.801,z:40.879},
	rotation: {x:0,y:0.7071078,z:0,w:-0.7071058},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Veg_Bush_02_b_Art__1__01 //
const s0_Veg_Bush_02_b_Art__1__01 = engine.addEntity()
GltfContainer.create(s0_Veg_Bush_02_b_Art__1__01, {
	src: "unity_assets/s0_Veg_Bush_02_b_Art_01.glb",
})
Transform.create(s0_Veg_Bush_02_b_Art__1__01, {
	position: {x:15.61,y:-0.801,z:43.03},
	rotation: {x:0,y:0.5237636,z:0,w:-0.8518636},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Veg_Bush_02_b_Art__2__01 //
const s0_Veg_Bush_02_b_Art__2__01 = engine.addEntity()
GltfContainer.create(s0_Veg_Bush_02_b_Art__2__01, {
	src: "unity_assets/s0_Veg_Bush_02_b_Art_01.glb",
})
Transform.create(s0_Veg_Bush_02_b_Art__2__01, {
	position: {x:7.66,y:-0.801,z:40.33},
	rotation: {x:0,y:0.8070294,z:0,w:-0.5905113},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Terrain_Rock_2_Art_01 //
const s0_Terrain_Rock_2_Art_01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Rock_2_Art_01, {
	src: "unity_assets/s0_Terrain_Rock_2_Art_01.glb",
})
Transform.create(s0_Terrain_Rock_2_Art_01, {
	position: {x:6.180029,y:-1.525879E-05,z:45.88387},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Terrain_Rock_2_Art__1__01 //
const s0_Terrain_Rock_2_Art__1__01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Rock_2_Art__1__01, {
	src: "unity_assets/s0_Terrain_Rock_2_Art_01.glb",
})
Transform.create(s0_Terrain_Rock_2_Art__1__01, {
	position: {x:4.2552,y:-0.41225,z:49.584},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:2.0076,y:2.0076,z:1.714852}
})


// Entity: s0_Terrain_Rock_2_Art__2__01 //
const s0_Terrain_Rock_2_Art__2__01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Rock_2_Art__2__01, {
	src: "unity_assets/s0_Terrain_Rock_2_Art_01.glb",
})
Transform.create(s0_Terrain_Rock_2_Art__2__01, {
	position: {x:4.804,y:-0.54,z:46.783},
	rotation: {x:0,y:0.7771609,z:0,w:0.6293018},
	scale: {x:1.244572,y:1.244572,z:1.063088}
})


// Entity: s0_Veg_Fruit_Art_01 //
const s0_Veg_Fruit_Art_01 = engine.addEntity()
GltfContainer.create(s0_Veg_Fruit_Art_01, {
	src: "unity_assets/s0_Veg_Fruit_Art_01.glb",
})
Transform.create(s0_Veg_Fruit_Art_01, {
	position: {x:10.58494,y:0.147,z:45.85641},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:2.478301,y:4.891671,z:2.478301}
})

