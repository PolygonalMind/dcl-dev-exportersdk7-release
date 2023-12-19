import { engine, Transform, GltfContainer, Animator, pointerEventsSystem, InputAction } from "@dcl/sdk/ecs"
import { movePlayerTo } from "~system/RestrictedActions"
import * as utils from "@dcl-sdk/utils"



// Entity: s0_Environment_01 //
const s0_Environment_01 = engine.addEntity()
Transform.create(s0_Environment_01, {
	position: {x:0,y:0,z:0},
	rotation: {x:0,y:0,z:0,w:1},
	scale: {x:1,y:1,z:1}
})


// Entity: s0_Bench_01__1__01 //
const s0_Bench_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Bench_01__1__01, {
	src: "unity_assets/s0_Bench_01__1__01.glb",
})
Transform.create(s0_Bench_01__1__01, {
	position: {x:6.074,y:0,z:22.849},
	rotation: {x:0,y:0.8449512,z:0,w:-0.5348436},
	scale: {x:1.1047,y:1,z:1.0874},
	parent: s0_Environment_01
})


// Entity: s0_Bench_01__2__01 //
const s0_Bench_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Bench_01__2__01, {
	src: "unity_assets/s0_Bench_01__1__01.glb",
})
Transform.create(s0_Bench_01__2__01, {
	position: {x:21.01,y:0,z:13.31},
	rotation: {x:0,y:0.9386941,z:0,w:0.3447514},
	scale: {x:1.1047,y:1,z:1.0874},
	parent: s0_Environment_01
})


// Entity: s0_Bench_01__3__01 //
const s0_Bench_01__3__01 = engine.addEntity()
GltfContainer.create(s0_Bench_01__3__01, {
	src: "unity_assets/s0_Bench_01__1__01.glb",
})
Transform.create(s0_Bench_01__3__01, {
	position: {x:6.45,y:0,z:75.71},
	rotation: {x:0,y:0.1707855,z:0,w:0.9853082},
	scale: {x:1.1047,y:1,z:1.0874},
	parent: s0_Environment_01
})


// Entity: s0_Brick_Step_01__1__01 //
const s0_Brick_Step_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Brick_Step_01__1__01, {
	src: "unity_assets/s0_Brick_Step_01__1__01.glb",
})
Transform.create(s0_Brick_Step_01__1__01, {
	position: {x:2.37,y:0,z:27.4},
	rotation: {x:0.558632,y:0.4129938,z:-0.3635057,w:0.6206692},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Brick_Step_01__2__01 //
const s0_Brick_Step_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Brick_Step_01__2__01, {
	src: "unity_assets/s0_Brick_Step_01__1__01.glb",
})
Transform.create(s0_Brick_Step_01__2__01, {
	position: {x:1.96,y:-0.21,z:26.23},
	rotation: {x:0.5976515,y:0.3368049,z:-0.2949895,w:0.6650989},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Bush_01__1__01 //
const s0_Bush_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Bush_01__1__01, {
	src: "unity_assets/s0_Bush_01__1__01.glb",
})
Transform.create(s0_Bush_01__1__01, {
	position: {x:4.4,y:-0.43,z:7.43},
	rotation: {x:0,y:0.965926,z:0,w:-0.2588186},
	scale: {x:1.5568,y:1.5568,z:1.5568},
	parent: s0_Environment_01
})


// Entity: s0_Bush_02__1__01 //
const s0_Bush_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Bush_02__1__01, {
	src: "unity_assets/s0_Bush_02__1__01.glb",
})
Transform.create(s0_Bush_02__1__01, {
	position: {x:26.47,y:0,z:5.49},
	rotation: {x:0,y:0.9914449,z:0,w:0.1305259},
	scale: {x:1.3712,y:1.3712,z:1.3712},
	parent: s0_Environment_01
})


// Entity: s0_Bush_03__1__01 //
const s0_Bush_03__1__01 = engine.addEntity()
GltfContainer.create(s0_Bush_03__1__01, {
	src: "unity_assets/s0_Bush_03__1__01.glb",
})
Transform.create(s0_Bush_03__1__01, {
	position: {x:27.22,y:0,z:18.31},
	rotation: {x:0,y:0.9238798,z:0,w:-0.3826828},
	scale: {x:1,y:1,z:1.0813},
	parent: s0_Environment_01
})


// Entity: s0_Entity_Bobcat_01__1__01 //
const s0_Entity_Bobcat_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Entity_Bobcat_01__1__01, {
	src: "unity_assets/s0_Entity_Bobcat_01__1__01.glb",
})
Transform.create(s0_Entity_Bobcat_01__1__01, {
	position: {x:20.421,y:0,z:22.239},
	rotation: {x:0,y:0.9111993,z:0,w:0.4119657},
	scale: {x:1.7425,y:1.7425,z:1.7425},
	parent: s0_Environment_01
})
Animator.create(s0_Entity_Bobcat_01__1__01,{
	states:[{
	clip: "Bobcat_Sleep_01_Anim",
	playing: true,
	loop: true,
	speed: 1
	},{
	clip: "Bobcat_Idle_01_Anim",
	playing: false,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Entity_Bobcat_02__1__01 //
const s0_Entity_Bobcat_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Entity_Bobcat_02__1__01, {
	src: "unity_assets/s0_Entity_Bobcat_02__1__01.glb",
})
Transform.create(s0_Entity_Bobcat_02__1__01, {
	position: {x:4.28,y:0,z:10.77},
	rotation: {x:0,y:0.9238797,z:0,w:-0.3826832},
	scale: {x:1.6364,y:1.6364,z:1.6364},
	parent: s0_Environment_01
})
Animator.create(s0_Entity_Bobcat_02__1__01,{
	states:[{
	clip: "Bobcat_Sleep_01_Anim",
	playing: true,
	loop: true,
	speed: 1
	},{
	clip: "Bobcat_Idle_01_Anim",
	playing: false,
	loop: true,
	speed: 1
	},]
})
pointerEventsSystem.onPointerDown(
	{entity: s0_Entity_Bobcat_02__1__01, opts: {button: InputAction.IA_ANY, maxDistance: 8, hoverText:"Wake up!"}},
	function(cmd){
		if(cmd.button === InputAction.IA_POINTER){
			Animator.playSingleAnimation(s0_Entity_Bobcat_02__1__01, "Bobcat_Idle_01_Anim", true)
		}
	}
)


// Entity: s0_Entity_Deer_01__1__01 //
const s0_Entity_Deer_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Entity_Deer_01__1__01, {
	src: "unity_assets/s0_Entity_Deer_01__1__01.glb",
})
Transform.create(s0_Entity_Deer_01__1__01, {
	position: {x:25.88,y:0,z:105.83},
	rotation: {x:0,y:0.2588187,z:0,w:0.9659259},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Entity_Deer_01__1__01,{
	states:[{
	clip: "Deer_Idle_01_Anim",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Entity_Deer_01__2__01 //
const s0_Entity_Deer_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Entity_Deer_01__2__01, {
	src: "unity_assets/s0_Entity_Deer_01__1__01.glb",
})
Transform.create(s0_Entity_Deer_01__2__01, {
	position: {x:15.158,y:3.948,z:58.652},
	rotation: {x:0,y:0.6087611,z:0,w:0.7933537},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Entity_Deer_01__2__01,{
	states:[{
	clip: "Deer_Idle_01_Anim",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Entity_Owl_01__1__01 //
const s0_Entity_Owl_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Entity_Owl_01__1__01, {
	src: "unity_assets/s0_Entity_Owl_01__1__01.glb",
})
Transform.create(s0_Entity_Owl_01__1__01, {
	position: {x:16.899,y:1.555,z:30.306},
	rotation: {x:0,y:0.8344606,z:0,w:0.5510677},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Entity_Owl_01__1__01,{
	states:[{
	clip: "Owl_Idle_01",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Entity_Owl_01__2__01 //
const s0_Entity_Owl_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Entity_Owl_01__2__01, {
	src: "unity_assets/s0_Entity_Owl_01__1__01.glb",
})
Transform.create(s0_Entity_Owl_01__2__01, {
	position: {x:17.028,y:1.537,z:30.702},
	rotation: {x:0,y:0.8344606,z:0,w:0.5510677},
	scale: {x:0.71716,y:0.71716,z:0.71716},
	parent: s0_Environment_01
})
Animator.create(s0_Entity_Owl_01__2__01,{
	states:[{
	clip: "Owl_Idle_01",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Entity_Owl_01__3__01 //
const s0_Entity_Owl_01__3__01 = engine.addEntity()
GltfContainer.create(s0_Entity_Owl_01__3__01, {
	src: "unity_assets/s0_Entity_Owl_01__1__01.glb",
})
Transform.create(s0_Entity_Owl_01__3__01, {
	position: {x:16.083,y:1.537,z:32.123},
	rotation: {x:0,y:0.1812781,z:0,w:0.9834319},
	scale: {x:0.7171599,y:0.71716,z:0.7171599},
	parent: s0_Environment_01
})
Animator.create(s0_Entity_Owl_01__3__01,{
	states:[{
	clip: "Owl_Idle_01",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Entity_Owl_01__4__01 //
const s0_Entity_Owl_01__4__01 = engine.addEntity()
GltfContainer.create(s0_Entity_Owl_01__4__01, {
	src: "unity_assets/s0_Entity_Owl_01__1__01.glb",
})
Transform.create(s0_Entity_Owl_01__4__01, {
	position: {x:16.372,y:1.5506,z:31.9583},
	rotation: {x:0,y:0.1812781,z:0,w:0.9834319},
	scale: {x:0.4915855,y:0.4915856,z:0.4915855},
	parent: s0_Environment_01
})
Animator.create(s0_Entity_Owl_01__4__01,{
	states:[{
	clip: "Owl_Idle_01",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Entity_Pig_01__1__01 //
const s0_Entity_Pig_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Entity_Pig_01__1__01, {
	src: "unity_assets/s0_Entity_Pig_01__1__01.glb",
})
Transform.create(s0_Entity_Pig_01__1__01, {
	position: {x:5.23,y:0,z:25.12},
	rotation: {x:0,y:0.9238797,z:0,w:-0.3826832},
	scale: {x:1.6629,y:1.6629,z:1.6629},
	parent: s0_Environment_01
})
Animator.create(s0_Entity_Pig_01__1__01,{
	states:[{
	clip: "Pig_Idle_01_Anim",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Entity_Pig_01__2__01 //
const s0_Entity_Pig_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Entity_Pig_01__2__01, {
	src: "unity_assets/s0_Entity_Pig_01__1__01.glb",
})
Transform.create(s0_Entity_Pig_01__2__01, {
	position: {x:4.717,y:0,z:26.433},
	rotation: {x:0,y:0.8247277,z:0,w:-0.5655301},
	scale: {x:0.9867815,y:0.9867815,z:0.9867815},
	parent: s0_Environment_01
})
Animator.create(s0_Entity_Pig_01__2__01,{
	states:[{
	clip: "Pig_Idle_01_Anim",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Fence_01__1__01 //
const s0_Fence_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Fence_01__1__01, {
	src: "unity_assets/s0_Fence_01__1__01.glb",
})
Transform.create(s0_Fence_01__1__01, {
	position: {x:1.622,y:8.058,z:33.328},
	rotation: {x:0,y:0.7149547,z:0,w:-0.6991708},
	scale: {x:1.695373,y:1.695373,z:1.695373},
	parent: s0_Environment_01
})


// Entity: s0_Fence_01__2__01 //
const s0_Fence_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Fence_01__2__01, {
	src: "unity_assets/s0_Fence_01__1__01.glb",
})
Transform.create(s0_Fence_01__2__01, {
	position: {x:1.622,y:4.75,z:33.328},
	rotation: {x:0,y:0.7149547,z:0,w:-0.6991708},
	scale: {x:1.695373,y:1.695373,z:1.695373},
	parent: s0_Environment_01
})


// Entity: s0_Fence_01__3__01 //
const s0_Fence_01__3__01 = engine.addEntity()
GltfContainer.create(s0_Fence_01__3__01, {
	src: "unity_assets/s0_Fence_01__1__01.glb",
})
Transform.create(s0_Fence_01__3__01, {
	position: {x:1.622,y:5.11,z:46.44},
	rotation: {x:0,y:0.7149547,z:0,w:-0.6991708},
	scale: {x:1.695373,y:1.695373,z:1.695373},
	parent: s0_Environment_01
})


// Entity: s0_Fence_01_Post__1__01 //
const s0_Fence_01_Post__1__01 = engine.addEntity()
GltfContainer.create(s0_Fence_01_Post__1__01, {
	src: "unity_assets/s0_Fence_01_Post__1__01.glb",
})
Transform.create(s0_Fence_01_Post__1__01, {
	position: {x:2.01,y:0,z:1.12},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Fence_01_Post__2__01 //
const s0_Fence_01_Post__2__01 = engine.addEntity()
GltfContainer.create(s0_Fence_01_Post__2__01, {
	src: "unity_assets/s0_Fence_01_Post__1__01.glb",
})
Transform.create(s0_Fence_01_Post__2__01, {
	position: {x:5.51,y:0,z:1.12},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Fence_01_Post__3__01 //
const s0_Fence_01_Post__3__01 = engine.addEntity()
GltfContainer.create(s0_Fence_01_Post__3__01, {
	src: "unity_assets/s0_Fence_01_Post__1__01.glb",
})
Transform.create(s0_Fence_01_Post__3__01, {
	position: {x:29.79,y:0,z:1.12},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Floating_Island_01__1__01 //
const s0_Floating_Island_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Floating_Island_01__1__01, {
	src: "unity_assets/s0_Floating_Island_01__1__01.glb",
})
Transform.create(s0_Floating_Island_01__1__01, {
	position: {x:17.88,y:0,z:48.9},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Floating_Island_02__1__01 //
const s0_Floating_Island_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Floating_Island_02__1__01, {
	src: "unity_assets/s0_Floating_Island_02__1__01.glb",
})
Transform.create(s0_Floating_Island_02__1__01, {
	position: {x:20.01,y:14.02,z:55.22},
	rotation: {x:0,y:0.7340077,z:0,w:0.6791411},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Floating_Island_02__2__01 //
const s0_Floating_Island_02__2__01 = engine.addEntity()
GltfContainer.create(s0_Floating_Island_02__2__01, {
	src: "unity_assets/s0_Floating_Island_02__1__01.glb",
})
Transform.create(s0_Floating_Island_02__2__01, {
	position: {x:23.2962,y:0.86,z:45.21},
	rotation: {x:0.679141,y:2.078712E-06,z:0.7340078,w:1.9297E-06},
	scale: {x:1,y:0.9552763,z:0.9592999},
	parent: s0_Environment_01
})


// Entity: s0_Floating_Island_02__3__01 //
const s0_Floating_Island_02__3__01 = engine.addEntity()
GltfContainer.create(s0_Floating_Island_02__3__01, {
	src: "unity_assets/s0_Floating_Island_02__1__01.glb",
})
Transform.create(s0_Floating_Island_02__3__01, {
	position: {x:5.9,y:-0.27,z:15.96},
	rotation: {x:0.679141,y:2.078712E-06,z:0.7340078,w:1.9297E-06},
	scale: {x:0.46967,y:0.1771462,z:0.46967},
	parent: s0_Environment_01
})


// Entity: s0_Floating_Island_02__4__01 //
const s0_Floating_Island_02__4__01 = engine.addEntity()
GltfContainer.create(s0_Floating_Island_02__4__01, {
	src: "unity_assets/s0_Floating_Island_02__1__01.glb",
})
Transform.create(s0_Floating_Island_02__4__01, {
	position: {x:6.35,y:-0.27,z:102.06},
	rotation: {x:6.761798E-07,y:0.9752398,z:-2.971348E-06,w:-0.2211499},
	scale: {x:0.4696701,y:0.1771462,z:0.4696701},
	parent: s0_Environment_01
})


// Entity: s0_Floating_Island_03__1__01 //
const s0_Floating_Island_03__1__01 = engine.addEntity()
GltfContainer.create(s0_Floating_Island_03__1__01, {
	src: "unity_assets/s0_Floating_Island_03__1__01.glb",
})
Transform.create(s0_Floating_Island_03__1__01, {
	position: {x:27.0641,y:-0.28,z:50.8201},
	rotation: {x:0,y:0.9914449,z:0,w:-0.130526},
	scale: {x:0.9265999,y:1,z:0.9865999},
	parent: s0_Environment_01
})


// Entity: s0_Floating_Island_03__2__01 //
const s0_Floating_Island_03__2__01 = engine.addEntity()
GltfContainer.create(s0_Floating_Island_03__2__01, {
	src: "unity_assets/s0_Floating_Island_03__1__01.glb",
})
Transform.create(s0_Floating_Island_03__2__01, {
	position: {x:30.384,y:2.88,z:58.194},
	rotation: {x:0,y:0.965926,z:0,w:0.2588186},
	scale: {x:0.70832,y:1.03797,z:0.70832},
	parent: s0_Environment_01
})


// Entity: s0_Floating_Island_03__3__01 //
const s0_Floating_Island_03__3__01 = engine.addEntity()
GltfContainer.create(s0_Floating_Island_03__3__01, {
	src: "unity_assets/s0_Floating_Island_03__1__01.glb",
})
Transform.create(s0_Floating_Island_03__3__01, {
	position: {x:23.47,y:7.0741,z:82.16},
	rotation: {x:0,y:0.8919956,z:0,w:0.4520442},
	scale: {x:0.8568777,y:1.255666,z:0.8568777},
	parent: s0_Environment_01
})


// Entity: s0_Floating_Island_04__1__01 //
const s0_Floating_Island_04__1__01 = engine.addEntity()
GltfContainer.create(s0_Floating_Island_04__1__01, {
	src: "unity_assets/s0_Floating_Island_04__1__01.glb",
})
Transform.create(s0_Floating_Island_04__1__01, {
	position: {x:16.16,y:-1.29,z:69.84},
	rotation: {x:0,y:0.9585373,z:0,w:-0.2849671},
	scale: {x:1.022,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_GodRay_01__1__01 //
const s0_GodRay_01__1__01 = engine.addEntity()
GltfContainer.create(s0_GodRay_01__1__01, {
	src: "unity_assets/s0_GodRay_01__1__01.glb",
})
Transform.create(s0_GodRay_01__1__01, {
	position: {x:13.94,y:10.83,z:80.87},
	rotation: {x:-0.02870386,y:0.7948074,z:0.1481511,w:-0.5877998},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_GodRay_01__2__01 //
const s0_GodRay_01__2__01 = engine.addEntity()
GltfContainer.create(s0_GodRay_01__2__01, {
	src: "unity_assets/s0_GodRay_01__2__01.glb",
})
Transform.create(s0_GodRay_01__2__01, {
	position: {x:13.94,y:10.83,z:80.87},
	rotation: {x:-0.112961,y:0.2727334,z:0.1000623,w:-0.9501809},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_GodRay_01__3__01 //
const s0_GodRay_01__3__01 = engine.addEntity()
GltfContainer.create(s0_GodRay_01__3__01, {
	src: "unity_assets/s0_GodRay_01__1__01.glb",
})
Transform.create(s0_GodRay_01__3__01, {
	position: {x:13.94,y:10.83,z:80.87},
	rotation: {x:-0.1481511,y:-0.5877995,z:-0.02870385,w:-0.7948077},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_GodRay_01__4__01 //
const s0_GodRay_01__4__01 = engine.addEntity()
GltfContainer.create(s0_GodRay_01__4__01, {
	src: "unity_assets/s0_GodRay_01__1__01.glb",
})
Transform.create(s0_GodRay_01__4__01, {
	position: {x:13.94,y:10.83,z:80.87},
	rotation: {x:-0.08446199,y:-0.9776509,z:-0.1250553,w:-0.1463773},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__37 //
const s0_Grass_01_a__1__Polybrush_Clone__37 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__37, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__37.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__37, {
	position: {x:10.67116,y:0,z:11.66254},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.9829597,y:1.193954,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__37,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__72 //
const s0_Grass_01_a__1__Polybrush_Clone__72 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__72, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__72, {
	position: {x:10.89468,y:0,z:13.35781},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.3528153,y:1.160533,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__72,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__47 //
const s0_Grass_01_a__1__Polybrush_Clone__47 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__47, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__47, {
	position: {x:10.48063,y:0,z:13.5322},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5173539,y:1.751468,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__47,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__40 //
const s0_Grass_01_a__1__Polybrush_Clone__40 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__40, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__40, {
	position: {x:10.40159,y:0,z:13.78043},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.3595312,y:1.567328,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__40,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__63 //
const s0_Grass_01_a__1__Polybrush_Clone__63 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__63, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__63, {
	position: {x:10.04434,y:0,z:15.10337},
	rotation: {x:0,y:-1,z:0,w:3.695528E-07},
	scale: {x:1.001583,y:1.076,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__63,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__46 //
const s0_Grass_01_a__1__Polybrush_Clone__46 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__46, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__46, {
	position: {x:9.410732,y:0,z:18.58442},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.193589,y:1.77239,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__46,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__02 //
const s0_Grass_01_a__1__Polybrush_Clone__02 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__02, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__02, {
	position: {x:9.377821,y:0,z:18.74703},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.4055399,y:1.60399,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__02,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__03 //
const s0_Grass_01_a__1__Polybrush_Clone__03 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__03, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__03, {
	position: {x:7.884151,y:0,z:20.37909},
	rotation: {x:0,y:-1,z:0,w:3.695528E-07},
	scale: {x:0.8767349,y:1.771207,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__03,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__11 //
const s0_Grass_01_a__1__Polybrush_Clone__11 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__11, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__11, {
	position: {x:5.252975,y:0,z:37.2846},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.9111564,y:1.265726,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__11,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__24 //
const s0_Grass_01_a__1__Polybrush_Clone__24 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__24, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__24, {
	position: {x:4.414614,y:0,z:37.46624},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.2977382,y:1.413702,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__24,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__53 //
const s0_Grass_01_a__1__Polybrush_Clone__53 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__53, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__53, {
	position: {x:5.111528,y:0,z:38.17435},
	rotation: {x:0,y:-1,z:0,w:3.695528E-07},
	scale: {x:1.140426,y:1.711211,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__53,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__48 //
const s0_Grass_01_a__1__Polybrush_Clone__48 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__48, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__48, {
	position: {x:5.223305,y:0,z:39.50171},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5201719,y:1.690576,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__48,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__05 //
const s0_Grass_01_a__1__Polybrush_Clone__05 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__05, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__05, {
	position: {x:4.650979,y:0,z:38.41335},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.7072455,y:1.411017,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__05,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__21 //
const s0_Grass_01_a__1__Polybrush_Clone__21 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__21, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__21, {
	position: {x:5.177244,y:0,z:36.94071},
	rotation: {x:0,y:-1,z:0,w:3.695528E-07},
	scale: {x:0.2936194,y:1.619726,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__21,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__41 //
const s0_Grass_01_a__1__Polybrush_Clone__41 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__41, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__41, {
	position: {x:20.82739,y:0,z:71.51619},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5765626,y:1.589136,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__41,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__16 //
const s0_Grass_01_a__1__Polybrush_Clone__16 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__16, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__16, {
	position: {x:21.0568,y:0,z:73.46837},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5889205,y:1.480434,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__16,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__44 //
const s0_Grass_01_a__1__Polybrush_Clone__44 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__44, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__44, {
	position: {x:21.66168,y:0,z:73.97825},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.6225067,y:1.562361,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__44,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__38 //
const s0_Grass_01_a__1__Polybrush_Clone__38 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__38, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__38, {
	position: {x:20.93668,y:0,z:74.76859},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5457166,y:1.203749,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__38,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__45 //
const s0_Grass_01_a__1__Polybrush_Clone__45 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__45, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__45, {
	position: {x:21.40165,y:0,z:69.87025},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.9887121,y:1.105501,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__45,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__14 //
const s0_Grass_01_a__1__Polybrush_Clone__14 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__14, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__14, {
	position: {x:24.85109,y:0,z:69.11621},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.9507087,y:1.777341,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__14,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__19 //
const s0_Grass_01_a__1__Polybrush_Clone__19 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__19, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__19, {
	position: {x:28.1342,y:0,z:68.94808},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.758577,y:1.659568,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__19,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__23 //
const s0_Grass_01_a__1__Polybrush_Clone__23 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__23, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__23, {
	position: {x:28.44816,y:0,z:70.51917},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.197707,y:1.062008,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__23,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__67 //
const s0_Grass_01_a__1__Polybrush_Clone__67 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__67, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__67, {
	position: {x:30.77947,y:0,z:73.96665},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.043529,y:1.268872,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__67,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__82 //
const s0_Grass_01_a__1__Polybrush_Clone__82 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__82, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__82, {
	position: {x:28.66291,y:0,z:74.35086},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.159687,y:1.725071,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__82,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__42 //
const s0_Grass_01_a__1__Polybrush_Clone__42 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__42, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__42, {
	position: {x:28.63763,y:0,z:96.31334},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.428334,y:1.093545,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__42,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__43 //
const s0_Grass_01_a__1__Polybrush_Clone__43 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__43, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__43, {
	position: {x:26.7461,y:0,z:96.86425},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.8265351,y:1.664769,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__43,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__80 //
const s0_Grass_01_a__1__Polybrush_Clone__80 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__80, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__80, {
	position: {x:26.84386,y:0,z:95.48779},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5281984,y:1.13646,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__80,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__83 //
const s0_Grass_01_a__1__Polybrush_Clone__83 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__83, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__83, {
	position: {x:28.866,y:0,z:92.97942},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.6531608,y:1.671252,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__83,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__64 //
const s0_Grass_01_a__1__Polybrush_Clone__64 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__64, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__64, {
	position: {x:28.21908,y:0,z:95.9333},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.6358761,y:1.507376,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__64,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__70 //
const s0_Grass_01_a__1__Polybrush_Clone__70 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__70, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__70, {
	position: {x:25.99593,y:0,z:99.33128},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5103945,y:1.137075,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__70,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__17 //
const s0_Grass_01_a__1__Polybrush_Clone__17 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__17, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__17, {
	position: {x:24.55461,y:0,z:100.2306},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.8933547,y:1.167633,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__17,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__36 //
const s0_Grass_01_a__1__Polybrush_Clone__36 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__36, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__36, {
	position: {x:24.27238,y:0,z:100.3515},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.6279966,y:1.636273,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__36,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__55 //
const s0_Grass_01_a__1__Polybrush_Clone__55 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__55, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__55, {
	position: {x:21.4044,y:0,z:101.2064},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5995816,y:1.451618,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__55,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__13 //
const s0_Grass_01_a__1__Polybrush_Clone__13 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__13, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__13, {
	position: {x:21.30105,y:0,z:102.7448},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.6650429,y:1.060058,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__13,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__86 //
const s0_Grass_01_a__1__Polybrush_Clone__86 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__86, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__86, {
	position: {x:23.43084,y:0,z:100.9324},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.9140858,y:1.095644,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__86,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__09 //
const s0_Grass_01_a__1__Polybrush_Clone__09 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__09, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__09, {
	position: {x:23.74387,y:0,z:100.8373},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.9526201,y:1.517941,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__09,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__51 //
const s0_Grass_01_a__1__Polybrush_Clone__51 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__51, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__51, {
	position: {x:25.78129,y:0,z:101.6956},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.7675732,y:1.709031,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__51,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__79 //
const s0_Grass_01_a__1__Polybrush_Clone__79 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__79, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__79, {
	position: {x:25.4222,y:0,z:101.7717},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.8804926,y:1.049162,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__79,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__20 //
const s0_Grass_01_a__1__Polybrush_Clone__20 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__20, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__20, {
	position: {x:25.91928,y:0,z:102.337},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.163399,y:1.303117,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__20,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__35 //
const s0_Grass_01_a__1__Polybrush_Clone__35 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__35, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__35, {
	position: {x:25.8878,y:0,z:103.0891},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.7603573,y:1.062825,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__35,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__81 //
const s0_Grass_01_a__1__Polybrush_Clone__81 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__81, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__81, {
	position: {x:22.13967,y:0,z:105.8854},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5536188,y:1.650375,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__81,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__12 //
const s0_Grass_01_a__1__Polybrush_Clone__12 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__12, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__12, {
	position: {x:23.09731,y:0,z:107.2577},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.8124262,y:1.13346,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__12,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__39 //
const s0_Grass_01_a__1__Polybrush_Clone__39 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__39, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__39, {
	position: {x:23.85589,y:0,z:107.7913},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.197626,y:1.364504,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__39,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__62 //
const s0_Grass_01_a__1__Polybrush_Clone__62 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__62, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__62, {
	position: {x:21.827,y:0,z:107.6388},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.388472,y:1.258265,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__62,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__28 //
const s0_Grass_01_a__1__Polybrush_Clone__28 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__28, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__28, {
	position: {x:21.90908,y:0,z:108.4654},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.6662738,y:1.512982,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__28,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__32 //
const s0_Grass_01_a__1__Polybrush_Clone__32 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__32, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__32, {
	position: {x:24.10621,y:0,z:108.9371},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.3688318,y:1.008066,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__32,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__25 //
const s0_Grass_01_a__1__Polybrush_Clone__25 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__25, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__25, {
	position: {x:23.48708,y:0,z:109.6325},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.665925,y:1.743645,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__25,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__85 //
const s0_Grass_01_a__1__Polybrush_Clone__85 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__85, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__85, {
	position: {x:21.71525,y:0,z:107.8258},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.4353227,y:1.642877,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__85,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__54 //
const s0_Grass_01_a__1__Polybrush_Clone__54 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__54, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__54, {
	position: {x:20.73696,y:0,z:108.4969},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.4297087,y:1.516889,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__54,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__27 //
const s0_Grass_01_a__1__Polybrush_Clone__27 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__27, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__27, {
	position: {x:21.00499,y:0,z:109.455},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.3301246,y:1.227797,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__27,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__29 //
const s0_Grass_01_a__1__Polybrush_Clone__29 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__29, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__29, {
	position: {x:23.00669,y:0,z:110.3091},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.077415,y:1.635171,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__29,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__57 //
const s0_Grass_01_a__1__Polybrush_Clone__57 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__57, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__57, {
	position: {x:23.31639,y:0,z:110.3113},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5058684,y:1.100817,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__57,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__06 //
const s0_Grass_01_a__1__Polybrush_Clone__06 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__06, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__06, {
	position: {x:18.6988,y:0,z:109.0919},
	rotation: {x:0,y:-1,z:0,w:3.695528E-07},
	scale: {x:0.349551,y:1.393005,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__06,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__10 //
const s0_Grass_01_a__1__Polybrush_Clone__10 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__10, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__10, {
	position: {x:6.530392,y:0,z:95.38505},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.3268197,y:1.259022,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__10,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__73 //
const s0_Grass_01_a__1__Polybrush_Clone__73 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__73, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__73, {
	position: {x:8.836865,y:0,z:95.08517},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5230055,y:1.599087,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__73,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__08 //
const s0_Grass_01_a__1__Polybrush_Clone__08 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__08, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__08, {
	position: {x:10.16198,y:0,z:94.93268},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.7117537,y:1.540244,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__08,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__33 //
const s0_Grass_01_a__1__Polybrush_Clone__33 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__33, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__33, {
	position: {x:12.06874,y:0,z:96.00017},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.216122,y:1.621239,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__33,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__30 //
const s0_Grass_01_a__1__Polybrush_Clone__30 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__30, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__30, {
	position: {x:10.7503,y:0,z:96.66379},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.20608,y:1.663117,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__30,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__68 //
const s0_Grass_01_a__1__Polybrush_Clone__68 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__68, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__68, {
	position: {x:6.182225,y:0,z:95.26234},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.7109232,y:1.167225,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__68,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__26 //
const s0_Grass_01_a__1__Polybrush_Clone__26 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__26, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__26, {
	position: {x:6.3734,y:0,z:96.1321},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.068653,y:1.706764,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__26,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__34 //
const s0_Grass_01_a__1__Polybrush_Clone__34 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__34, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__34, {
	position: {x:9.717648,y:0,z:96.90257},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5946658,y:1.605682,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__34,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__76 //
const s0_Grass_01_a__1__Polybrush_Clone__76 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__76, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__76, {
	position: {x:11.25914,y:0,z:97.9594},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.7747613,y:1.19539,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__76,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__78 //
const s0_Grass_01_a__1__Polybrush_Clone__78 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__78, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__78, {
	position: {x:10.70746,y:0,z:97.44086},
	rotation: {x:0,y:-1,z:0,w:3.695528E-07},
	scale: {x:0.8362223,y:1.595989,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__78,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__01 //
const s0_Grass_01_a__1__Polybrush_Clone__01 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__01, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__01, {
	position: {x:10.9843,y:0,z:98.30575},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.8099986,y:1.328772,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__01,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__04 //
const s0_Grass_01_a__1__Polybrush_Clone__04 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__04, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__04, {
	position: {x:10.49926,y:0,z:97.20655},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.2926393,y:1.768445,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__04,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__07 //
const s0_Grass_01_a__1__Polybrush_Clone__07 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__07, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__07, {
	position: {x:8.677421,y:0,z:95.54959},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.090625,y:1.304776,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__07,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__22 //
const s0_Grass_01_a__1__Polybrush_Clone__22 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__22, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__22, {
	position: {x:6.324256,y:0,z:95.41679},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.208818,y:1.795549,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__22,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__74 //
const s0_Grass_01_a__1__Polybrush_Clone__74 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__74, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__74, {
	position: {x:4.878881,y:0,z:94.14579},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.753755,y:1.600785,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__74,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__31 //
const s0_Grass_01_a__1__Polybrush_Clone__31 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__31, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__31, {
	position: {x:3.800483,y:0,z:94.14156},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.396125,y:1.336367,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__31,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__59 //
const s0_Grass_01_a__1__Polybrush_Clone__59 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__59, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__59, {
	position: {x:4.580802,y:0,z:93.47689},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.6076384,y:1.627992,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__59,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__15 //
const s0_Grass_01_a__1__Polybrush_Clone__15 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__15, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__15, {
	position: {x:10.50772,y:0,z:10.45767},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.5139659,y:1.440445,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__15,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__50 //
const s0_Grass_01_a__1__Polybrush_Clone__50 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__50, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__50, {
	position: {x:10.83239,y:0,z:13.09345},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.4486243,y:1.749212,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__50,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__61 //
const s0_Grass_01_a__1__Polybrush_Clone__61 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__61, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__61.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__61, {
	position: {x:10.84452,y:0.4059197,z:11.52537},
	rotation: {x:0.3159913,y:-0.9487621,z:4.143725E-08,w:1.244151E-07},
	scale: {x:0.4420859,y:1.511685,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__61,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__77 //
const s0_Grass_01_a__1__Polybrush_Clone__77 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__77, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__77, {
	position: {x:9.683335,y:0,z:10.93722},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.805135,y:1.216847,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__77,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__69 //
const s0_Grass_01_a__1__Polybrush_Clone__69 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__69, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__69, {
	position: {x:7.921413,y:0,z:3.375804},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.4951408,y:1.297046,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__69,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__65 //
const s0_Grass_01_a__1__Polybrush_Clone__65 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__65, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__65, {
	position: {x:8.778932,y:0,z:3.298365},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.9119424,y:1.265433,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__65,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__52 //
const s0_Grass_01_a__1__Polybrush_Clone__52 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__52, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__52, {
	position: {x:9.136017,y:0,z:2.645134},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.229362,y:1.146616,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__52,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__84 //
const s0_Grass_01_a__1__Polybrush_Clone__84 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__84, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__84, {
	position: {x:9.855813,y:0,z:3.040752},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.7018844,y:1.666036,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__84,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__71 //
const s0_Grass_01_a__1__Polybrush_Clone__71 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__71, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__71.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__71, {
	position: {x:11.19481,y:0,z:1.862133},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.9507943,y:1.186023,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__71,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__56 //
const s0_Grass_01_a__1__Polybrush_Clone__56 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__56, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__56, {
	position: {x:11.15072,y:0,z:3.247663},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.3603191,y:1.124419,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__56,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__60 //
const s0_Grass_01_a__1__Polybrush_Clone__60 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__60, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__60, {
	position: {x:11.35728,y:0,z:2.528822},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.4935675,y:1.214691,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__60,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__87 //
const s0_Grass_01_a__1__Polybrush_Clone__87 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__87, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__87, {
	position: {x:11.78685,y:0,z:2.486577},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.7468232,y:1.043414,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__87,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__49 //
const s0_Grass_01_a__1__Polybrush_Clone__49 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__49, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__49, {
	position: {x:23.7866,y:0,z:3.171232},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.179828,y:1.769186,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__49,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__75 //
const s0_Grass_01_a__1__Polybrush_Clone__75 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__75, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__75.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__75, {
	position: {x:24.59664,y:0,z:2.3565},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.8233142,y:1.478502,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__75,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__58 //
const s0_Grass_01_a__1__Polybrush_Clone__58 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__58, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__58.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__58, {
	position: {x:26.29894,y:0,z:1.052821},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.4689221,y:1.700444,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__58,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__66 //
const s0_Grass_01_a__1__Polybrush_Clone__66 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__66, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__66, {
	position: {x:27.19741,y:0,z:1.899981},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:0.2916557,y:1.812574,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__66,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_a__1__Polybrush_Clone__18 //
const s0_Grass_01_a__1__Polybrush_Clone__18 = engine.addEntity()
GltfContainer.create(s0_Grass_01_a__1__Polybrush_Clone__18, {
	src: "unity_assets/s0_Grass_01_a__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_a__1__Polybrush_Clone__18, {
	position: {x:23.84317,y:0,z:3.645778},
	rotation: {x:0,y:-1,z:0,w:1.311342E-07},
	scale: {x:1.078486,y:1.296752,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_a__1__Polybrush_Clone__18,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__81 //
const s0_Grass_01_b__1__Polybrush_Clone__81 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__81, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__81, {
	position: {x:11.82256,y:0,z:11.74362},
	rotation: {x:0,y:0.8431138,z:0,w:-0.5377353},
	scale: {x:1.056408,y:1.043467,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__81,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__72 //
const s0_Grass_01_b__1__Polybrush_Clone__72 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__72, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__72.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__72, {
	position: {x:12.01156,y:0,z:10.266},
	rotation: {x:0,y:0.3083219,z:0,w:-0.9512821},
	scale: {x:1.029158,y:1.663291,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__72,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__39 //
const s0_Grass_01_b__1__Polybrush_Clone__39 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__39, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__39, {
	position: {x:10.70642,y:0,z:11.74868},
	rotation: {x:0,y:0.5575007,z:0,w:-0.8301764},
	scale: {x:1.137761,y:1.358475,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__39,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__83 //
const s0_Grass_01_b__1__Polybrush_Clone__83 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__83, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__83, {
	position: {x:11.39226,y:0,z:11.03608},
	rotation: {x:0,y:0.9303938,z:0,w:-0.3665617},
	scale: {x:1.16225,y:1.124787,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__83,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__26 //
const s0_Grass_01_b__1__Polybrush_Clone__26 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__26, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__26, {
	position: {x:11.48091,y:0,z:11.29926},
	rotation: {x:0,y:0.9230785,z:0,w:-0.3846116},
	scale: {x:1.229772,y:0.617067,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__26,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__58 //
const s0_Grass_01_b__1__Polybrush_Clone__58 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__58, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__58.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__58, {
	position: {x:10.96259,y:0,z:15.48055},
	rotation: {x:0,y:-0.2726335,z:0,w:-0.962118},
	scale: {x:1.280576,y:1.54297,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__58,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__76 //
const s0_Grass_01_b__1__Polybrush_Clone__76 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__76, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__76.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__76, {
	position: {x:7.912305,y:0,z:20.76794},
	rotation: {x:0,y:0.515488,z:0,w:-0.8568968},
	scale: {x:1.045355,y:1.355494,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__76,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__14 //
const s0_Grass_01_b__1__Polybrush_Clone__14 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__14, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__14, {
	position: {x:8.60259,y:0,z:20.06957},
	rotation: {x:0,y:-0.1618664,z:0,w:-0.9868127},
	scale: {x:1.218406,y:1.240876,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__14,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__79 //
const s0_Grass_01_b__1__Polybrush_Clone__79 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__79, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__79, {
	position: {x:3.445284,y:0,z:37.37357},
	rotation: {x:0,y:-0.3198049,z:0,w:-0.9474834},
	scale: {x:1.085306,y:1.499941,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__79,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__07 //
const s0_Grass_01_b__1__Polybrush_Clone__07 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__07, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__07, {
	position: {x:4.955447,y:0,z:37.84555},
	rotation: {x:0,y:-0.06150293,z:0,w:-0.9981069},
	scale: {x:1.2282,y:1.550723,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__07,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__55 //
const s0_Grass_01_b__1__Polybrush_Clone__55 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__55, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__55, {
	position: {x:5.266439,y:0,z:38.52589},
	rotation: {x:0,y:0.3172202,z:0,w:-0.9483519},
	scale: {x:1.081074,y:1.012204,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__55,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__09 //
const s0_Grass_01_b__1__Polybrush_Clone__09 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__09, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__09, {
	position: {x:4.586162,y:0,z:40.13395},
	rotation: {x:0,y:0.712109,z:0,w:-0.7020689},
	scale: {x:1.278594,y:0.9586268,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__09,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__51 //
const s0_Grass_01_b__1__Polybrush_Clone__51 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__51, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__51, {
	position: {x:3.82831,y:0,z:40.44804},
	rotation: {x:0,y:-0.9762278,z:0,w:-0.2167469},
	scale: {x:1.199008,y:1.082359,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__51,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__74 //
const s0_Grass_01_b__1__Polybrush_Clone__74 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__74, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__74, {
	position: {x:3.107209,y:0,z:40.23897},
	rotation: {x:0,y:-0.9996161,z:0,w:-0.02770581},
	scale: {x:1.120672,y:1.307497,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__74,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__18 //
const s0_Grass_01_b__1__Polybrush_Clone__18 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__18, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__18, {
	position: {x:5.061476,y:0,z:35.96569},
	rotation: {x:0,y:-0.8984119,z:0,w:-0.4391537},
	scale: {x:1.189004,y:1.573035,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__18,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__61 //
const s0_Grass_01_b__1__Polybrush_Clone__61 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__61, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__61.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__61, {
	position: {x:8.839805,y:0,z:51.99006},
	rotation: {x:0,y:-0.3806714,z:0,w:-0.9247105},
	scale: {x:1.054117,y:0.730065,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__61,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__45 //
const s0_Grass_01_b__1__Polybrush_Clone__45 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__45, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__45, {
	position: {x:21.65007,y:0,z:70.98938},
	rotation: {x:0,y:0.9978395,z:0,w:-0.06569928},
	scale: {x:1.19174,y:1.603168,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__45,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__65 //
const s0_Grass_01_b__1__Polybrush_Clone__65 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__65, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__65, {
	position: {x:21.54627,y:0,z:71.92747},
	rotation: {x:0,y:-0.1745816,z:0,w:-0.9846427},
	scale: {x:1.09198,y:0.9021924,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__65,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__53 //
const s0_Grass_01_b__1__Polybrush_Clone__53 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__53, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__53, {
	position: {x:20.21802,y:0,z:71.61189},
	rotation: {x:0,y:0.9765613,z:0,w:-0.2152394},
	scale: {x:1.237134,y:1.270332,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__53,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__69 //
const s0_Grass_01_b__1__Polybrush_Clone__69 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__69, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__69, {
	position: {x:20.96312,y:0,z:73.77605},
	rotation: {x:0,y:-0.9629058,z:0,w:-0.2698379},
	scale: {x:1.12755,y:1.547923,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__69,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__01 //
const s0_Grass_01_b__1__Polybrush_Clone__01 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__01, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__01, {
	position: {x:20.97752,y:0,z:74.1871},
	rotation: {x:0,y:-0.6608672,z:0,w:-0.7505029},
	scale: {x:1.025862,y:1.358242,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__01,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__63 //
const s0_Grass_01_b__1__Polybrush_Clone__63 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__63, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__63, {
	position: {x:20.02758,y:0,z:71.94925},
	rotation: {x:0,y:0.9852154,z:0,w:-0.1713202},
	scale: {x:1.059477,y:1.255477,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__63,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__35 //
const s0_Grass_01_b__1__Polybrush_Clone__35 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__35, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__35, {
	position: {x:21.60983,y:0,z:71.45786},
	rotation: {x:0,y:-0.8359977,z:0,w:-0.5487329},
	scale: {x:1.088077,y:1.459779,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__35,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__75 //
const s0_Grass_01_b__1__Polybrush_Clone__75 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__75, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__75, {
	position: {x:21.43852,y:0,z:73.87598},
	rotation: {x:0,y:0.6514485,z:0,w:-0.7586928},
	scale: {x:1.052232,y:1.027238,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__75,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__87 //
const s0_Grass_01_b__1__Polybrush_Clone__87 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__87, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__87, {
	position: {x:21.07815,y:0,z:75.13913},
	rotation: {x:0,y:-0.953861,z:0,w:-0.3002486},
	scale: {x:1.229975,y:1.576489,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__87,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__85 //
const s0_Grass_01_b__1__Polybrush_Clone__85 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__85, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__85, {
	position: {x:21.60985,y:0,z:74.68118},
	rotation: {x:0,y:-0.9775461,z:0,w:-0.2107218},
	scale: {x:1.248967,y:1.192034,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__85,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__67 //
const s0_Grass_01_b__1__Polybrush_Clone__67 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__67, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__67, {
	position: {x:24.13826,y:0,z:68.26587},
	rotation: {x:0,y:-0.7029878,z:0,w:-0.7112019},
	scale: {x:1.1207,y:0.9148932,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__67,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__16 //
const s0_Grass_01_b__1__Polybrush_Clone__16 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__16, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__16, {
	position: {x:26.71745,y:0,z:69.46064},
	rotation: {x:0,y:0.8640639,z:0,w:-0.5033821},
	scale: {x:1.140086,y:1.542477,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__16,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__66 //
const s0_Grass_01_b__1__Polybrush_Clone__66 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__66, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__66, {
	position: {x:29.59452,y:0,z:71.80766},
	rotation: {x:0,y:-0.1512935,z:0,w:-0.9884889},
	scale: {x:1.085767,y:0.5341378,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__66,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__41 //
const s0_Grass_01_b__1__Polybrush_Clone__41 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__41, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__41, {
	position: {x:28.91031,y:0,z:71.06656},
	rotation: {x:0,y:-0.8940728,z:0,w:-0.4479215},
	scale: {x:1.039486,y:1.612563,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__41,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__77 //
const s0_Grass_01_b__1__Polybrush_Clone__77 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__77, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__77, {
	position: {x:29.68145,y:0,z:72.51489},
	rotation: {x:0,y:-0.7161477,z:0,w:-0.6979488},
	scale: {x:1.027215,y:1.565474,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__77,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__32 //
const s0_Grass_01_b__1__Polybrush_Clone__32 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__32, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__32, {
	position: {x:31.09874,y:0,z:73.02},
	rotation: {x:0,y:0.9567838,z:0,w:-0.2908004},
	scale: {x:1.230416,y:1.645806,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__32,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__27 //
const s0_Grass_01_b__1__Polybrush_Clone__27 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__27, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__27, {
	position: {x:29.45899,y:0,z:73.21724},
	rotation: {x:0,y:-0.4957247,z:0,w:-0.8684797},
	scale: {x:1.285419,y:1.287001,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__27,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__37 //
const s0_Grass_01_b__1__Polybrush_Clone__37 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__37, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__37, {
	position: {x:29.35751,y:0,z:75.36221},
	rotation: {x:0,y:0.2109471,z:0,w:-0.9774975},
	scale: {x:1.01628,y:1.240413,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__37,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__89 //
const s0_Grass_01_b__1__Polybrush_Clone__89 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__89, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__89, {
	position: {x:29.52126,y:0,z:76.44174},
	rotation: {x:0,y:-0.75567,z:0,w:-0.6549525},
	scale: {x:1.298138,y:1.021592,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__89,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__02 //
const s0_Grass_01_b__1__Polybrush_Clone__02 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__02, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__02, {
	position: {x:29.1393,y:0,z:91.68361},
	rotation: {x:0,y:-0.4991682,z:0,w:-0.8665051},
	scale: {x:1.164512,y:0.7214947,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__02,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__08 //
const s0_Grass_01_b__1__Polybrush_Clone__08 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__08, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__08, {
	position: {x:28.29003,y:0,z:93.38177},
	rotation: {x:0,y:-0.9072201,z:0,w:-0.4206561},
	scale: {x:1.205589,y:1.390662,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__08,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__12 //
const s0_Grass_01_b__1__Polybrush_Clone__12 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__12, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__12, {
	position: {x:28.08198,y:0,z:94.34716},
	rotation: {x:0,y:-0.9824225,z:0,w:-0.1866709},
	scale: {x:1.053725,y:0.4880525,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__12,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__24 //
const s0_Grass_01_b__1__Polybrush_Clone__24 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__24, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__24.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__24, {
	position: {x:28.53893,y:0,z:93.9377},
	rotation: {x:0,y:-0.953254,z:0,w:-0.3021703},
	scale: {x:1.176519,y:1.530728,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__24,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__31 //
const s0_Grass_01_b__1__Polybrush_Clone__31 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__31, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__31, {
	position: {x:28.69326,y:0,z:95.69135},
	rotation: {x:0,y:0.5607333,z:0,w:-0.8279964},
	scale: {x:1.134911,y:1.316599,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__31,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__91 //
const s0_Grass_01_b__1__Polybrush_Clone__91 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__91, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__91, {
	position: {x:28.68009,y:0,z:96.55662},
	rotation: {x:0,y:0.1227773,z:0,w:-0.9924343},
	scale: {x:1.23808,y:1.491174,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__91,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__50 //
const s0_Grass_01_b__1__Polybrush_Clone__50 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__50, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__50.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__50, {
	position: {x:28.24836,y:0,z:96.75301},
	rotation: {x:0,y:-0.08362835,z:0,w:-0.996497},
	scale: {x:1.174546,y:0.6661796,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__50,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__56 //
const s0_Grass_01_b__1__Polybrush_Clone__56 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__56, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__56, {
	position: {x:27.73106,y:0,z:97.8487},
	rotation: {x:0,y:-0.9559932,z:0,w:-0.293389},
	scale: {x:1.0017,y:1.073506,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__56,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__38 //
const s0_Grass_01_b__1__Polybrush_Clone__38 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__38, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__38, {
	position: {x:28.59611,y:0,z:94.3933},
	rotation: {x:0,y:-0.2496339,z:0,w:-0.9683403},
	scale: {x:1.147857,y:0.6257677,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__38,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__19 //
const s0_Grass_01_b__1__Polybrush_Clone__19 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__19, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__19, {
	position: {x:29.31505,y:0,z:93.7793},
	rotation: {x:0,y:0.9962499,z:0,w:-0.08652306},
	scale: {x:1.122463,y:1.160878,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__19,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__86 //
const s0_Grass_01_b__1__Polybrush_Clone__86 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__86, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__86, {
	position: {x:29.13647,y:0,z:93.60784},
	rotation: {x:0,y:0.2379704,z:0,w:-0.9712724},
	scale: {x:1.279245,y:1.369243,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__86,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__29 //
const s0_Grass_01_b__1__Polybrush_Clone__29 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__29, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__29, {
	position: {x:29.14343,y:0,z:95.02819},
	rotation: {x:0,y:-0.9991385,z:0,w:-0.04150163},
	scale: {x:1.109295,y:1.207,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__29,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__43 //
const s0_Grass_01_b__1__Polybrush_Clone__43 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__43, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__43, {
	position: {x:27.79754,y:0,z:97.26983},
	rotation: {x:0,y:-0.913873,z:0,w:-0.4060002},
	scale: {x:1.286108,y:0.8561696,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__43,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__20 //
const s0_Grass_01_b__1__Polybrush_Clone__20 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__20, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__20, {
	position: {x:27.67102,y:0,z:98.51938},
	rotation: {x:0,y:0.9969102,z:0,w:-0.07854979},
	scale: {x:1.016463,y:0.8463796,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__20,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__03 //
const s0_Grass_01_b__1__Polybrush_Clone__03 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__03, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__03, {
	position: {x:20.25905,y:0,z:101.5503},
	rotation: {x:0,y:-0.4983738,z:0,w:-0.8669623},
	scale: {x:1.172168,y:0.6965109,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__03,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__48 //
const s0_Grass_01_b__1__Polybrush_Clone__48 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__48, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__48, {
	position: {x:20.93172,y:0,z:102.5764},
	rotation: {x:0,y:-0.7287056,z:0,w:-0.6848272},
	scale: {x:1.174665,y:1.470248,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__48,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__34 //
const s0_Grass_01_b__1__Polybrush_Clone__34 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__34, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__34, {
	position: {x:25.26037,y:0,z:101.8005},
	rotation: {x:0,y:0.006579383,z:0,w:-0.9999784},
	scale: {x:1.06371,y:1.260598,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__34,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__11 //
const s0_Grass_01_b__1__Polybrush_Clone__11 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__11, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__11, {
	position: {x:23.61121,y:0,z:102.2788},
	rotation: {x:0,y:0.5658598,z:0,w:-0.8245015},
	scale: {x:1.250438,y:1.192513,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__11,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__57 //
const s0_Grass_01_b__1__Polybrush_Clone__57 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__57, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__57, {
	position: {x:23.65405,y:0,z:101.5968},
	rotation: {x:0,y:-0.8390458,z:0,w:-0.5440608},
	scale: {x:1.085214,y:0.8754828,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__57,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__88 //
const s0_Grass_01_b__1__Polybrush_Clone__88 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__88, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__88, {
	position: {x:24.95094,y:0,z:102.0726},
	rotation: {x:0,y:0.04898474,z:0,w:-0.9987996},
	scale: {x:1.072104,y:0.6842082,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__88,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__10 //
const s0_Grass_01_b__1__Polybrush_Clone__10 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__10, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__10, {
	position: {x:25.96347,y:0,z:101.7529},
	rotation: {x:0,y:-0.7029507,z:0,w:-0.7112386},
	scale: {x:1.198871,y:1.110053,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__10,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__44 //
const s0_Grass_01_b__1__Polybrush_Clone__44 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__44, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__44, {
	position: {x:22.60201,y:0,z:107.7983},
	rotation: {x:0,y:-0.937071,z:0,w:-0.349139},
	scale: {x:1.069691,y:1.678357,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__44,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__21 //
const s0_Grass_01_b__1__Polybrush_Clone__21 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__21, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__21, {
	position: {x:20.51401,y:0,z:108.0858},
	rotation: {x:0,y:-0.5207694,z:0,w:-0.8536975},
	scale: {x:1.110325,y:1.644387,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__21,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__30 //
const s0_Grass_01_b__1__Polybrush_Clone__30 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__30, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__30, {
	position: {x:22.10355,y:0,z:108.8776},
	rotation: {x:0,y:-0.8219264,z:0,w:-0.5695937},
	scale: {x:1.047092,y:0.6164117,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__30,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__84 //
const s0_Grass_01_b__1__Polybrush_Clone__84 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__84, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__84, {
	position: {x:23.51235,y:0,z:108.0856},
	rotation: {x:0,y:-0.972898,z:0,w:-0.2312347},
	scale: {x:1.129485,y:0.5025226,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__84,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__04 //
const s0_Grass_01_b__1__Polybrush_Clone__04 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__04, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__04, {
	position: {x:23.29059,y:0,z:108.97},
	rotation: {x:0,y:-0.1870419,z:0,w:-0.982352},
	scale: {x:1.174068,y:1.656222,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__04,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__78 //
const s0_Grass_01_b__1__Polybrush_Clone__78 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__78, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__78, {
	position: {x:21.49508,y:0,z:109.6353},
	rotation: {x:0,y:-0.01612507,z:0,w:-0.99987},
	scale: {x:1.026303,y:1.108684,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__78,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__17 //
const s0_Grass_01_b__1__Polybrush_Clone__17 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__17, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__17, {
	position: {x:23.91859,y:0,z:109.111},
	rotation: {x:0,y:0.9179662,z:0,w:-0.3966588},
	scale: {x:1.147433,y:1.599791,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__17,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__23 //
const s0_Grass_01_b__1__Polybrush_Clone__23 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__23, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__23, {
	position: {x:24.13587,y:0,z:109.6711},
	rotation: {x:0,y:0.9231818,z:0,w:-0.3843637},
	scale: {x:1.012533,y:1.040689,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__23,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__60 //
const s0_Grass_01_b__1__Polybrush_Clone__60 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__60, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__60, {
	position: {x:21.02442,y:0,z:109.5546},
	rotation: {x:0,y:0.9115937,z:0,w:-0.4110924},
	scale: {x:1.165159,y:0.8502142,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__60,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__54 //
const s0_Grass_01_b__1__Polybrush_Clone__54 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__54, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__54, {
	position: {x:12.25319,y:0,z:96.12393},
	rotation: {x:0,y:-0.06118595,z:0,w:-0.9981264},
	scale: {x:1.017513,y:0.7197378,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__54,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__73 //
const s0_Grass_01_b__1__Polybrush_Clone__73 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__73, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__73, {
	position: {x:13.0229,y:0,z:95.82904},
	rotation: {x:0,y:-0.9942664,z:0,w:-0.1069315},
	scale: {x:1.280499,y:1.504558,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__73,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__33 //
const s0_Grass_01_b__1__Polybrush_Clone__33 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__33, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__33, {
	position: {x:9.326177,y:0,z:95.74876},
	rotation: {x:0,y:-0.9988981,z:0,w:-0.04693105},
	scale: {x:1.107265,y:1.25469,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__33,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__52 //
const s0_Grass_01_b__1__Polybrush_Clone__52 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__52, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__52, {
	position: {x:10.6614,y:0,z:96.81399},
	rotation: {x:0,y:-0.3539595,z:0,w:-0.9352608},
	scale: {x:1.278514,y:0.677603,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__52,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__46 //
const s0_Grass_01_b__1__Polybrush_Clone__46 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__46, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__46.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__46, {
	position: {x:11.32046,y:0,z:98.123},
	rotation: {x:0,y:-0.9112859,z:0,w:-0.4117741},
	scale: {x:1.05167,y:1.386913,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__46,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__05 //
const s0_Grass_01_b__1__Polybrush_Clone__05 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__05, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__05.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__05, {
	position: {x:5.993363,y:0,z:95.2347},
	rotation: {x:0,y:0.9438603,z:0,w:-0.3303451},
	scale: {x:1.028222,y:1.434924,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__05,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__49 //
const s0_Grass_01_b__1__Polybrush_Clone__49 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__49, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__49, {
	position: {x:5.109025,y:0,z:94.54384},
	rotation: {x:0,y:0.9413528,z:0,w:-0.3374241},
	scale: {x:1.086674,y:1.158819,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__49,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__70 //
const s0_Grass_01_b__1__Polybrush_Clone__70 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__70, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__70, {
	position: {x:3.801668,y:0,z:94.643},
	rotation: {x:0,y:0.9928144,z:0,w:-0.1196642},
	scale: {x:1.179281,y:1.445416,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__70,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__68 //
const s0_Grass_01_b__1__Polybrush_Clone__68 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__68, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__68, {
	position: {x:11.18061,y:0,z:11.48091},
	rotation: {x:0,y:0.9080371,z:0,w:-0.4188898},
	scale: {x:1.06309,y:1.331421,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__68,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__22 //
const s0_Grass_01_b__1__Polybrush_Clone__22 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__22, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__22.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__22, {
	position: {x:10.90891,y:0.4541656,z:11.66254},
	rotation: {x:0.2234395,y:-0.6708761,z:0.6708762,w:-0.2234395},
	scale: {x:1.045334,y:1.496749,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__22,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__90 //
const s0_Grass_01_b__1__Polybrush_Clone__90 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__90, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__90, {
	position: {x:10.27006,y:0,z:12.10734},
	rotation: {x:0,y:0.230304,z:0,w:-0.9731187},
	scale: {x:1.182856,y:0.8058078,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__90,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__15 //
const s0_Grass_01_b__1__Polybrush_Clone__15 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__15, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__15, {
	position: {x:11.18436,y:0,z:11.35243},
	rotation: {x:0,y:-0.291061,z:0,w:-0.9567046},
	scale: {x:1.156993,y:0.728223,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__15,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__13 //
const s0_Grass_01_b__1__Polybrush_Clone__13 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__13, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__13, {
	position: {x:10.14042,y:0,z:13.10103},
	rotation: {x:0,y:0.5231287,z:0,w:-0.8522537},
	scale: {x:1.061671,y:1.081623,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__13,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__36 //
const s0_Grass_01_b__1__Polybrush_Clone__36 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__36, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__36, {
	position: {x:10.75388,y:0,z:11.75247},
	rotation: {x:0,y:-0.4583011,z:0,w:-0.888797},
	scale: {x:1.203397,y:1.199291,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__36,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__92 //
const s0_Grass_01_b__1__Polybrush_Clone__92 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__92, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__92, {
	position: {x:7.328424,y:0,z:2.889033},
	rotation: {x:0,y:-0.99987,z:0,w:-0.01612461},
	scale: {x:1.062407,y:0.8588135,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__92,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__47 //
const s0_Grass_01_b__1__Polybrush_Clone__47 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__47, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__47, {
	position: {x:8.844428,y:0,z:2.704177},
	rotation: {x:0,y:0.7894355,z:0,w:-0.6138336},
	scale: {x:1.220292,y:0.7685251,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__47,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__42 //
const s0_Grass_01_b__1__Polybrush_Clone__42 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__42, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__42.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__42, {
	position: {x:8.851573,y:0,z:2.45473},
	rotation: {x:0,y:-0.9625847,z:0,w:-0.270981},
	scale: {x:1.29589,y:1.394754,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__42,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__25 //
const s0_Grass_01_b__1__Polybrush_Clone__25 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__25, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__25.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__25, {
	position: {x:8.76922,y:0.3500399,z:2.404374},
	rotation: {x:0.1481214,y:-0.5126033,z:0.6914189,w:-0.4870706},
	scale: {x:1.290817,y:0.5461955,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__25,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__71 //
const s0_Grass_01_b__1__Polybrush_Clone__71 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__71, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__71, {
	position: {x:11.13286,y:0,z:2.765703},
	rotation: {x:0,y:-0.6842075,z:0,w:-0.7292874},
	scale: {x:1.075627,y:1.025715,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__71,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__40 //
const s0_Grass_01_b__1__Polybrush_Clone__40 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__40, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__40, {
	position: {x:11.79696,y:0,z:2.038703},
	rotation: {x:0,y:0.5714262,z:0,w:-0.8206534},
	scale: {x:1.248164,y:0.832217,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__40,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__28 //
const s0_Grass_01_b__1__Polybrush_Clone__28 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__28, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__28, {
	position: {x:22.18679,y:0,z:2.338078},
	rotation: {x:0,y:0.9999281,z:0,w:-0.01199746},
	scale: {x:1.032648,y:1.069023,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__28,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__82 //
const s0_Grass_01_b__1__Polybrush_Clone__82 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__82, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__82, {
	position: {x:21.64855,y:0,z:2.720251},
	rotation: {x:0,y:-0.3778188,z:0,w:-0.9258796},
	scale: {x:1.026398,y:1.649047,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__82,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__59 //
const s0_Grass_01_b__1__Polybrush_Clone__59 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__59, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__59, {
	position: {x:22.97511,y:0,z:2.009745},
	rotation: {x:0,y:0.8531569,z:0,w:-0.5216546},
	scale: {x:1.252983,y:1.446178,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__59,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__06 //
const s0_Grass_01_b__1__Polybrush_Clone__06 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__06, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__06, {
	position: {x:24.78811,y:0,z:2.321566},
	rotation: {x:0,y:-0.5552635,z:0,w:-0.8316745},
	scale: {x:1.104971,y:0.9040493,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__06,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__62 //
const s0_Grass_01_b__1__Polybrush_Clone__62 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__62, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__62, {
	position: {x:26.3398,y:0,z:1.837666},
	rotation: {x:0,y:-0.9859731,z:0,w:-0.1669042},
	scale: {x:1.250968,y:1.52487,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__62,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__80 //
const s0_Grass_01_b__1__Polybrush_Clone__80 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__80, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__80, {
	position: {x:26.57405,y:0,z:1.232045},
	rotation: {x:0,y:0.5508048,z:0,w:-0.8346341},
	scale: {x:1.151302,y:0.8932747,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__80,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Grass_01_b__1__Polybrush_Clone__64 //
const s0_Grass_01_b__1__Polybrush_Clone__64 = engine.addEntity()
GltfContainer.create(s0_Grass_01_b__1__Polybrush_Clone__64, {
	src: "unity_assets/s0_Grass_01_b__1__Polybrush_Clone__81.glb",
})
Transform.create(s0_Grass_01_b__1__Polybrush_Clone__64, {
	position: {x:25.76987,y:0,z:2.447577},
	rotation: {x:0,y:0.3145714,z:0,w:-0.9492338},
	scale: {x:1.223484,y:0.962837,z:1},
	parent: s0_Environment_01
})
Animator.create(s0_Grass_01_b__1__Polybrush_Clone__64,{
	states:[{
	clip: "Grass_b_flow",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Land_Floor__1__01 //
const s0_Land_Floor__1__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__1__01, {
	src: "unity_assets/s0_Land_Floor__1__01.glb",
})
Transform.create(s0_Land_Floor__1__01, {
	position: {x:8,y:0,z:8},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__2__01 //
const s0_Land_Floor__2__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__2__01, {
	src: "unity_assets/s0_Land_Floor__2__01.glb",
})
Transform.create(s0_Land_Floor__2__01, {
	position: {x:23.95,y:0,z:8},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__3__01 //
const s0_Land_Floor__3__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__3__01, {
	src: "unity_assets/s0_Land_Floor__3__01.glb",
})
Transform.create(s0_Land_Floor__3__01, {
	position: {x:8,y:0,z:23.95},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__4__01 //
const s0_Land_Floor__4__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__4__01, {
	src: "unity_assets/s0_Land_Floor__4__01.glb",
})
Transform.create(s0_Land_Floor__4__01, {
	position: {x:23.95,y:0,z:23.95},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__5__01 //
const s0_Land_Floor__5__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__5__01, {
	src: "unity_assets/s0_Land_Floor__5__01.glb",
})
Transform.create(s0_Land_Floor__5__01, {
	position: {x:8,y:0,z:39.9},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__6__01 //
const s0_Land_Floor__6__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__6__01, {
	src: "unity_assets/s0_Land_Floor__2__01.glb",
})
Transform.create(s0_Land_Floor__6__01, {
	position: {x:23.95,y:0,z:39.9},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__7__01 //
const s0_Land_Floor__7__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__7__01, {
	src: "unity_assets/s0_Land_Floor__2__01.glb",
})
Transform.create(s0_Land_Floor__7__01, {
	position: {x:8,y:0,z:55.85},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__8__01 //
const s0_Land_Floor__8__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__8__01, {
	src: "unity_assets/s0_Land_Floor__2__01.glb",
})
Transform.create(s0_Land_Floor__8__01, {
	position: {x:23.95,y:0,z:55.85},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__9__01 //
const s0_Land_Floor__9__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__9__01, {
	src: "unity_assets/s0_Land_Floor__9__01.glb",
})
Transform.create(s0_Land_Floor__9__01, {
	position: {x:8,y:0,z:71.8},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__10__01 //
const s0_Land_Floor__10__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__10__01, {
	src: "unity_assets/s0_Land_Floor__10__01.glb",
})
Transform.create(s0_Land_Floor__10__01, {
	position: {x:23.95,y:0,z:71.8},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__11__01 //
const s0_Land_Floor__11__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__11__01, {
	src: "unity_assets/s0_Land_Floor__2__01.glb",
})
Transform.create(s0_Land_Floor__11__01, {
	position: {x:8,y:0,z:87.74999},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__12__01 //
const s0_Land_Floor__12__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__12__01, {
	src: "unity_assets/s0_Land_Floor__12__01.glb",
})
Transform.create(s0_Land_Floor__12__01, {
	position: {x:23.95,y:0,z:87.74999},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__13__01 //
const s0_Land_Floor__13__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__13__01, {
	src: "unity_assets/s0_Land_Floor__13__01.glb",
})
Transform.create(s0_Land_Floor__13__01, {
	position: {x:8,y:0,z:103.7},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Land_Floor__14__01 //
const s0_Land_Floor__14__01 = engine.addEntity()
GltfContainer.create(s0_Land_Floor__14__01, {
	src: "unity_assets/s0_Land_Floor__14__01.glb",
})
Transform.create(s0_Land_Floor__14__01, {
	position: {x:23.95,y:0,z:103.7},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Log_01__1__01 //
const s0_Log_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Log_01__1__01, {
	src: "unity_assets/s0_Log_01__1__01.glb",
})
Transform.create(s0_Log_01__1__01, {
	position: {x:17.85,y:-1.08,z:41.89},
	rotation: {x:0,y:0.9914449,z:0,w:0.1305259},
	scale: {x:2.0076,y:2.0076,z:2.0076},
	parent: s0_Environment_01
})


// Entity: s0_Log_01__2__01 //
const s0_Log_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Log_01__2__01, {
	src: "unity_assets/s0_Log_01__1__01.glb",
})
Transform.create(s0_Log_01__2__01, {
	position: {x:2.62,y:-1.08,z:18.33},
	rotation: {x:0,y:0.6087629,z:0,w:0.7933523},
	scale: {x:2.0076,y:2.0076,z:2.0076},
	parent: s0_Environment_01
})


// Entity: s0_Mushroom_01__1__01 //
const s0_Mushroom_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Mushroom_01__1__01, {
	src: "unity_assets/s0_Mushroom_01__1__01.glb",
})
Transform.create(s0_Mushroom_01__1__01, {
	position: {x:10.32,y:0,z:55.42},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1.525457,y:1.525457,z:1.525457},
	parent: s0_Environment_01
})
Animator.create(s0_Mushroom_01__1__01,{
	states:[{
	clip: "Mushroom_Idle",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Mushroom_01__2__01 //
const s0_Mushroom_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Mushroom_01__2__01, {
	src: "unity_assets/s0_Mushroom_01__1__01.glb",
})
Transform.create(s0_Mushroom_01__2__01, {
	position: {x:10.971,y:0.155,z:56.743},
	rotation: {x:0,y:0.7746224,z:0,w:-0.6324241},
	scale: {x:1.525457,y:1.525457,z:1.525457},
	parent: s0_Environment_01
})
Animator.create(s0_Mushroom_01__2__01,{
	states:[{
	clip: "Mushroom_Idle",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Mushroom_01__3__01 //
const s0_Mushroom_01__3__01 = engine.addEntity()
GltfContainer.create(s0_Mushroom_01__3__01, {
	src: "unity_assets/s0_Mushroom_01__1__01.glb",
})
Transform.create(s0_Mushroom_01__3__01, {
	position: {x:10.144,y:-0.023,z:56.399},
	rotation: {x:0,y:0.7933535,z:0,w:-0.6087613},
	scale: {x:0.7569166,y:0.7569165,z:0.7569166},
	parent: s0_Environment_01
})
Animator.create(s0_Mushroom_01__3__01,{
	states:[{
	clip: "Mushroom_Idle",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Mushroom_02__1__01 //
const s0_Mushroom_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Mushroom_02__1__01, {
	src: "unity_assets/s0_Mushroom_02__1__01.glb",
})
Transform.create(s0_Mushroom_02__1__01, {
	position: {x:19.548,y:0,z:11.624},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:3.5809,y:3.744905,z:3.5809},
	parent: s0_Environment_01
})
Animator.create(s0_Mushroom_02__1__01,{
	states:[{
	clip: "Mushroom_Idle",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Mushroom_02__2__01 //
const s0_Mushroom_02__2__01 = engine.addEntity()
GltfContainer.create(s0_Mushroom_02__2__01, {
	src: "unity_assets/s0_Mushroom_02__1__01.glb",
})
Transform.create(s0_Mushroom_02__2__01, {
	position: {x:18.875,y:0,z:11.168},
	rotation: {x:0,y:-0.0834981,z:0,w:0.9965079},
	scale: {x:2.568078,y:2.685696,z:2.568078},
	parent: s0_Environment_01
})
Animator.create(s0_Mushroom_02__2__01,{
	states:[{
	clip: "Mushroom_Idle",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Mushroom_02__3__01 //
const s0_Mushroom_02__3__01 = engine.addEntity()
GltfContainer.create(s0_Mushroom_02__3__01, {
	src: "unity_assets/s0_Mushroom_02__1__01.glb",
})
Transform.create(s0_Mushroom_02__3__01, {
	position: {x:18.991,y:0,z:11.84},
	rotation: {x:0,y:0.2496219,z:0,w:0.9683434},
	scale: {x:1.523923,y:1.593719,z:1.523923},
	parent: s0_Environment_01
})
Animator.create(s0_Mushroom_02__3__01,{
	states:[{
	clip: "Mushroom_Idle",
	playing: true,
	loop: true,
	speed: 1
	},]
})


// Entity: s0_Path_01__1__01 //
const s0_Path_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Path_01__1__01, {
	src: "unity_assets/s0_Path_01__1__01.glb",
})
Transform.create(s0_Path_01__1__01, {
	position: {x:12.62,y:0,z:58.01},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Path_02__1__01 //
const s0_Path_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Path_02__1__01, {
	src: "unity_assets/s0_Path_02__1__01.glb",
})
Transform.create(s0_Path_02__1__01, {
	position: {x:10.54,y:0,z:75.37},
	rotation: {x:0,y:0.8660257,z:0,w:0.4999996},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Path_02__2__01 //
const s0_Path_02__2__01 = engine.addEntity()
GltfContainer.create(s0_Path_02__2__01, {
	src: "unity_assets/s0_Path_02__2__01.glb",
})
Transform.create(s0_Path_02__2__01, {
	position: {x:13.86,y:0,z:104.74},
	rotation: {x:0,y:0.649153,z:0,w:0.760658},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Path_03__1__01 //
const s0_Path_03__1__01 = engine.addEntity()
GltfContainer.create(s0_Path_03__1__01, {
	src: "unity_assets/s0_Path_03__1__01.glb",
})
Transform.create(s0_Path_03__1__01, {
	position: {x:26.496,y:0,z:85.237},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Path_04__1__01 //
const s0_Path_04__1__01 = engine.addEntity()
GltfContainer.create(s0_Path_04__1__01, {
	src: "unity_assets/s0_Path_04__1__01.glb",
})
Transform.create(s0_Path_04__1__01, {
	position: {x:15.2,y:-0.11,z:85.7},
	rotation: {x:0,y:0.965926,z:0,w:-0.2588186},
	scale: {x:1.7248,y:1.7248,z:1.7248},
	parent: s0_Environment_01
})


// Entity: s0_PathStep_Art_01 //
const s0_PathStep_Art_01 = engine.addEntity()
GltfContainer.create(s0_PathStep_Art_01, {
	src: "unity_assets/s0_PathStep_Art_01.glb",
})
Transform.create(s0_PathStep_Art_01, {
	position: {x:7.000069,y:0,z:69.22849},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:100,y:100,z:100},
	parent: s0_Environment_01
})


// Entity: s0_Rock_01__1__01 //
const s0_Rock_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Rock_01__1__01, {
	src: "unity_assets/s0_Rock_01__1__01.glb",
})
Transform.create(s0_Rock_01__1__01, {
	position: {x:13.03,y:0,z:56.03},
	rotation: {x:0,y:0.6811569,z:0,w:-0.7321374},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Rock_01__2__01 //
const s0_Rock_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Rock_01__2__01, {
	src: "unity_assets/s0_Rock_01__1__01.glb",
})
Transform.create(s0_Rock_01__2__01, {
	position: {x:20.52,y:1.53,z:47.37},
	rotation: {x:0,y:0.9881656,z:0,w:-0.1533909},
	scale: {x:1.915167,y:1.915167,z:1.915167},
	parent: s0_Environment_01
})


// Entity: s0_Rock_01__3__01 //
const s0_Rock_01__3__01 = engine.addEntity()
GltfContainer.create(s0_Rock_01__3__01, {
	src: "unity_assets/s0_Rock_01__1__01.glb",
})
Transform.create(s0_Rock_01__3__01, {
	position: {x:4.99,y:0,z:79.2},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Rock_02__1__01 //
const s0_Rock_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Rock_02__1__01, {
	src: "unity_assets/s0_Rock_02__1__01.glb",
})
Transform.create(s0_Rock_02__1__01, {
	position: {x:15.73,y:-3.8,z:104.97},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:5.384,y:5.384,z:5.384},
	parent: s0_Environment_01
})


// Entity: s0_Rock_03__1__01 //
const s0_Rock_03__1__01 = engine.addEntity()
GltfContainer.create(s0_Rock_03__1__01, {
	src: "unity_assets/s0_Rock_03__1__01.glb",
})
Transform.create(s0_Rock_03__1__01, {
	position: {x:27.53,y:-1.73,z:22.58},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Rock_03__2__01 //
const s0_Rock_03__2__01 = engine.addEntity()
GltfContainer.create(s0_Rock_03__2__01, {
	src: "unity_assets/s0_Rock_03__1__01.glb",
})
Transform.create(s0_Rock_03__2__01, {
	position: {x:3.4258,y:-1.05,z:22.13},
	rotation: {x:0,y:0.8660259,z:0,w:-0.4999992},
	scale: {x:1,y:1,z:0.7126},
	parent: s0_Environment_01
})


// Entity: s0_Rock_04__1__01 //
const s0_Rock_04__1__01 = engine.addEntity()
GltfContainer.create(s0_Rock_04__1__01, {
	src: "unity_assets/s0_Rock_04__1__01.glb",
})
Transform.create(s0_Rock_04__1__01, {
	position: {x:3.25,y:0,z:42},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Rock_05__1__01 //
const s0_Rock_05__1__01 = engine.addEntity()
GltfContainer.create(s0_Rock_05__1__01, {
	src: "unity_assets/s0_Rock_05__1__01.glb",
})
Transform.create(s0_Rock_05__1__01, {
	position: {x:24.84,y:0,z:42.79},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1.8839,y:1.8839,z:1.8839},
	parent: s0_Environment_01
})


// Entity: s0_Rock_05__2__01 //
const s0_Rock_05__2__01 = engine.addEntity()
GltfContainer.create(s0_Rock_05__2__01, {
	src: "unity_assets/s0_Rock_05__1__01.glb",
})
Transform.create(s0_Rock_05__2__01, {
	position: {x:5.05,y:0,z:13.07},
	rotation: {x:0,y:2.994141E-06,z:0,w:1},
	scale: {x:1.8839,y:1.8839,z:1.8839},
	parent: s0_Environment_01
})


// Entity: s0_Rock_06__1__01 //
const s0_Rock_06__1__01 = engine.addEntity()
GltfContainer.create(s0_Rock_06__1__01, {
	src: "unity_assets/s0_Rock_06__1__01.glb",
})
Transform.create(s0_Rock_06__1__01, {
	position: {x:25.41,y:-0.61,z:15},
	rotation: {x:0,y:0.965926,z:0,w:0.2588186},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Rock_06__2__01 //
const s0_Rock_06__2__01 = engine.addEntity()
GltfContainer.create(s0_Rock_06__2__01, {
	src: "unity_assets/s0_Rock_06__1__01.glb",
})
Transform.create(s0_Rock_06__2__01, {
	position: {x:6.15,y:-0.61,z:10.13},
	rotation: {x:0,y:0.9632025,z:0,w:-0.268777},
	scale: {x:0.59341,y:0.59341,z:0.59341},
	parent: s0_Environment_01
})


// Entity: s0_Root_01__1__01 //
const s0_Root_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Root_01__1__01, {
	src: "unity_assets/s0_Root_01__1__01.glb",
})
Transform.create(s0_Root_01__1__01, {
	position: {x:10.01,y:-0.71,z:5.89},
	rotation: {x:0.1955961,y:0.9203266,z:0.07041818,w:0.3313343},
	scale: {x:1.7425,y:1.7425,z:1.7425},
	parent: s0_Environment_01
})


// Entity: s0_Root_01__2__01 //
const s0_Root_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Root_01__2__01, {
	src: "unity_assets/s0_Root_01__1__01.glb",
})
Transform.create(s0_Root_01__2__01, {
	position: {x:21.89,y:-0.71,z:5.81},
	rotation: {x:0.1186432,y:0.5582444,z:-0.1707055,w:-0.8032103},
	scale: {x:1.7425,y:1.7425,z:1.7425},
	parent: s0_Environment_01
})


// Entity: s0_Root_01__3__01 //
const s0_Root_01__3__01 = engine.addEntity()
GltfContainer.create(s0_Root_01__3__01, {
	src: "unity_assets/s0_Root_01__1__01.glb",
})
Transform.create(s0_Root_01__3__01, {
	position: {x:21.89,y:-0.71,z:5.81},
	rotation: {x:0.04428596,y:0.208376,z:-0.2031141,w:-0.9557003},
	scale: {x:1.141843,y:1.141843,z:1.141843},
	parent: s0_Environment_01
})


// Entity: s0_Root_01__4__01 //
const s0_Root_01__4__01 = engine.addEntity()
GltfContainer.create(s0_Root_01__4__01, {
	src: "unity_assets/s0_Root_01__1__01.glb",
})
Transform.create(s0_Root_01__4__01, {
	position: {x:21.89,y:-0.71,z:5.81},
	rotation: {x:-0.004280779,y:-0.04691146,z:-0.3216281,w:-0.9456937},
	scale: {x:0.768426,y:0.7684261,z:0.7684261},
	parent: s0_Environment_01
})


// Entity: s0_Root_02__1__01 //
const s0_Root_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Root_02__1__01, {
	src: "unity_assets/s0_Root_02__1__01.glb",
})
Transform.create(s0_Root_02__1__01, {
	position: {x:14.613,y:15.875,z:58.26},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:0.7839062,y:0.7839062,z:0.7839062},
	parent: s0_Environment_01
})


// Entity: s0_Root_02__2__01 //
const s0_Root_02__2__01 = engine.addEntity()
GltfContainer.create(s0_Root_02__2__01, {
	src: "unity_assets/s0_Root_02__1__01.glb",
})
Transform.create(s0_Root_02__2__01, {
	position: {x:14.613,y:15.875,z:58.26},
	rotation: {x:-0.3420717,y:0.3646736,z:-0.8580011,w:0.1176192},
	scale: {x:0.7839062,y:0.7839062,z:0.7839062},
	parent: s0_Environment_01
})


// Entity: s0_Shelter_01__1__01 //
const s0_Shelter_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Shelter_01__1__01, {
	src: "unity_assets/s0_Shelter_01__1__01.glb",
})
Transform.create(s0_Shelter_01__1__01, {
	position: {x:23.43,y:-2.07,z:18.69},
	rotation: {x:0,y:0.9660104,z:0,w:0.2585033},
	scale: {x:3.2451,y:3.2451,z:3.2451},
	parent: s0_Environment_01
})


// Entity: s0_Signal_02__1__01 //
const s0_Signal_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Signal_02__1__01, {
	src: "unity_assets/s0_Signal_02__1__01.glb",
})
Transform.create(s0_Signal_02__1__01, {
	position: {x:5.569,y:-0.018,z:19.01},
	rotation: {x:0,y:-0.2588157,z:0,w:0.9659267},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Amphitheater_01__1__01 //
const s0_Str_Amphitheater_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Amphitheater_01__1__01, {
	src: "unity_assets/s0_Str_Amphitheater_01__1__01.glb",
})
Transform.create(s0_Str_Amphitheater_01__1__01, {
	position: {x:19.7834,y:0,z:32.1956},
	rotation: {x:0,y:0.5566277,z:0,w:0.8307621},
	scale: {x:1.026,y:1,z:0.7928},
	parent: s0_Environment_01
})


// Entity: s0_Str_Fountain_01__1__01 //
const s0_Str_Fountain_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Fountain_01__1__01, {
	src: "unity_assets/s0_Str_Fountain_01__1__01.glb",
})
Transform.create(s0_Str_Fountain_01__1__01, {
	position: {x:15.8,y:0,z:31},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1.0463,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_01__1__01 //
const s0_Str_Ruins_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_01__1__01, {
	src: "unity_assets/s0_Str_Ruins_01__1__01.glb",
})
Transform.create(s0_Str_Ruins_01__1__01, {
	position: {x:6.86,y:2.4,z:77.17},
	rotation: {x:0,y:0.3380598,z:0,w:0.9411247},
	scale: {x:1.3094,y:1.3094,z:1.3094},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_02__1__01 //
const s0_Str_Ruins_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_02__1__01, {
	src: "unity_assets/s0_Str_Ruins_02__1__01.glb",
})
Transform.create(s0_Str_Ruins_02__1__01, {
	position: {x:3.88,y:0,z:104.8},
	rotation: {x:0,y:0.296151,z:0,w:-0.9551411},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_03__1__01 //
const s0_Str_Ruins_03__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_03__1__01, {
	src: "unity_assets/s0_Str_Ruins_03__1__01.glb",
})
Transform.create(s0_Str_Ruins_03__1__01, {
	position: {x:1.53,y:0,z:63.77},
	rotation: {x:0,y:0.9318756,z:0,w:0.3627781},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_03__2__01 //
const s0_Str_Ruins_03__2__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_03__2__01, {
	src: "unity_assets/s0_Str_Ruins_03__1__01.glb",
})
Transform.create(s0_Str_Ruins_03__2__01, {
	position: {x:15.2,y:1.91,z:50.45},
	rotation: {x:0,y:0.9318756,z:0,w:0.3627781},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_03__3__01 //
const s0_Str_Ruins_03__3__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_03__3__01, {
	src: "unity_assets/s0_Str_Ruins_03__1__01.glb",
})
Transform.create(s0_Str_Ruins_03__3__01, {
	position: {x:26.22,y:0,z:73.24},
	rotation: {x:0,y:0.9318756,z:0,w:0.3627781},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_04_1__1__01 //
const s0_Str_Ruins_04_1__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_04_1__1__01, {
	src: "unity_assets/s0_Str_Ruins_04_1__1__01.glb",
})
Transform.create(s0_Str_Ruins_04_1__1__01, {
	position: {x:24.27,y:-0.25,z:85.17},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_04_1__2__01 //
const s0_Str_Ruins_04_1__2__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_04_1__2__01, {
	src: "unity_assets/s0_Str_Ruins_04_1__1__01.glb",
})
Transform.create(s0_Str_Ruins_04_1__2__01, {
	position: {x:28.776,y:-0.25,z:85.315},
	rotation: {x:0,y:0.7071066,z:0,w:0.707107},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_04_2__1__01 //
const s0_Str_Ruins_04_2__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_04_2__1__01, {
	src: "unity_assets/s0_Str_Ruins_04_2__1__01.glb",
})
Transform.create(s0_Str_Ruins_04_2__1__01, {
	position: {x:1.47,y:0,z:48.66},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_04_3__1__01 //
const s0_Str_Ruins_04_3__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_04_3__1__01, {
	src: "unity_assets/s0_Str_Ruins_04_3__1__01.glb",
})
Transform.create(s0_Str_Ruins_04_3__1__01, {
	position: {x:1.6,y:0,z:44.3},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_04_3__2__01 //
const s0_Str_Ruins_04_3__2__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_04_3__2__01, {
	src: "unity_assets/s0_Str_Ruins_04_3__1__01.glb",
})
Transform.create(s0_Str_Ruins_04_3__2__01, {
	position: {x:1.6,y:-0.39,z:40.17},
	rotation: {x:5.731954E-09,y:0.991365,z:-0.1311319,w:-4.333394E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_04_3__3__01 //
const s0_Str_Ruins_04_3__3__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_04_3__3__01, {
	src: "unity_assets/s0_Str_Ruins_04_3__1__01.glb",
})
Transform.create(s0_Str_Ruins_04_3__3__01, {
	position: {x:1.6,y:0,z:35.6},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_04_3__4__01 //
const s0_Str_Ruins_04_3__4__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_04_3__4__01, {
	src: "unity_assets/s0_Str_Ruins_04_3__1__01.glb",
})
Transform.create(s0_Str_Ruins_04_3__4__01, {
	position: {x:1.6,y:0,z:31.12},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__1__01 //
const s0_Str_Ruins_05__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__1__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__1__01, {
	position: {x:4.17,y:0,z:79.79},
	rotation: {x:0,y:0.9238797,z:0,w:-0.3826832},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__2__01 //
const s0_Str_Ruins_05__2__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__2__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__2__01, {
	position: {x:6.58,y:0,z:85.19},
	rotation: {x:0,y:0.3609992,z:0,w:-0.9325661},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__3__01 //
const s0_Str_Ruins_05__3__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__3__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__3__01, {
	position: {x:8.78,y:0,z:87.39},
	rotation: {x:0,y:0.3609992,z:0,w:-0.9325661},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__4__01 //
const s0_Str_Ruins_05__4__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__4__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__4__01, {
	position: {x:10.52,y:0,z:89.63},
	rotation: {x:0,y:0.3609992,z:0,w:-0.9325661},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__5__01 //
const s0_Str_Ruins_05__5__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__5__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__5__01, {
	position: {x:6.58,y:1.04,z:85.19},
	rotation: {x:0,y:0.3609992,z:0,w:-0.9325661},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__6__01 //
const s0_Str_Ruins_05__6__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__6__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__6__01, {
	position: {x:10.52,y:1.04,z:89.63},
	rotation: {x:0,y:0.3609992,z:0,w:-0.9325661},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__7__01 //
const s0_Str_Ruins_05__7__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__7__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__7__01, {
	position: {x:8.78,y:1.04,z:87.39},
	rotation: {x:0,y:0.3609992,z:0,w:-0.9325661},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__8__01 //
const s0_Str_Ruins_05__8__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__8__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__8__01, {
	position: {x:8.78,y:3.26,z:87.39},
	rotation: {x:0.4738102,y:6.048534E-08,z:-0.8806271,w:-1.671619E-07},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__9__01 //
const s0_Str_Ruins_05__9__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__9__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__9__01, {
	position: {x:6.58,y:2.09,z:85.19},
	rotation: {x:0,y:0.3609992,z:0,w:-0.9325661},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__10__01 //
const s0_Str_Ruins_05__10__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__10__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__10__01, {
	position: {x:10.52,y:2.09,z:89.63},
	rotation: {x:0,y:0.3609992,z:0,w:-0.9325661},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__11__01 //
const s0_Str_Ruins_05__11__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__11__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__11__01, {
	position: {x:6.58,y:3.15,z:85.19},
	rotation: {x:0,y:0.3609992,z:0,w:-0.9325661},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__12__01 //
const s0_Str_Ruins_05__12__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__12__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__12__01, {
	position: {x:10.52,y:3.15,z:89.63},
	rotation: {x:0,y:0.3609992,z:0,w:-0.9325661},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Ruins_05__13__01 //
const s0_Str_Ruins_05__13__01 = engine.addEntity()
GltfContainer.create(s0_Str_Ruins_05__13__01, {
	src: "unity_assets/s0_Str_Ruins_05__1__01.glb",
})
Transform.create(s0_Str_Ruins_05__13__01, {
	position: {x:8.78,y:3.15,z:87.39},
	rotation: {x:0,y:0.3609992,z:0,w:-0.9325661},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Skydoor_01__1__01 //
const s0_Str_Skydoor_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Skydoor_01__1__01, {
	src: "unity_assets/s0_Str_Skydoor_01__1__01.glb",
})
Transform.create(s0_Str_Skydoor_01__1__01, {
	position: {x:15.29,y:0,z:5.29},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Str_Statue_greek_01__1__01 //
const s0_Str_Statue_greek_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Statue_greek_01__1__01, {
	src: "unity_assets/s0_Str_Statue_greek_01__1__01.glb",
})
Transform.create(s0_Str_Statue_greek_01__1__01, {
	position: {x:13.85,y:0,z:43.05},
	rotation: {x:0,y:0.3826838,z:0,w:0.9238794},
	scale: {x:2.060645,y:2.060645,z:2.060645},
	parent: s0_Environment_01
})


// Entity: s0_Str_Statue_greek_01__2__01 //
const s0_Str_Statue_greek_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Str_Statue_greek_01__2__01, {
	src: "unity_assets/s0_Str_Statue_greek_01__1__01.glb",
})
Transform.create(s0_Str_Statue_greek_01__2__01, {
	position: {x:12.99,y:0,z:43.46},
	rotation: {x:0,y:0.3826838,z:0,w:0.9238794},
	scale: {x:-2.027757,y:2.060645,z:2.060645},
	parent: s0_Environment_01
})


// Entity: s0_Str_Statue_greek_01__3__01 //
const s0_Str_Statue_greek_01__3__01 = engine.addEntity()
GltfContainer.create(s0_Str_Statue_greek_01__3__01, {
	src: "unity_assets/s0_Str_Statue_greek_01__1__01.glb",
})
Transform.create(s0_Str_Statue_greek_01__3__01, {
	position: {x:13.089,y:3.149,z:44.546},
	rotation: {x:0.3034483,y:0.361453,z:-0.1256925,w:0.8726238},
	scale: {x:-2.027757,y:-2.234769,z:2.060645},
	parent: s0_Environment_01
})


// Entity: s0_Str_Statue_greek_02__1__01 //
const s0_Str_Statue_greek_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Str_Statue_greek_02__1__01, {
	src: "unity_assets/s0_Str_Statue_greek_02__1__01.glb",
})
Transform.create(s0_Str_Statue_greek_02__1__01, {
	position: {x:26.28,y:1.12,z:73.1},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:2.4938,y:2.4938,z:2.4938},
	parent: s0_Environment_01
})


// Entity: s0_Terrain_Amount_01__1__01 //
const s0_Terrain_Amount_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Amount_01__1__01, {
	src: "unity_assets/s0_Terrain_Amount_01__1__01.glb",
})
Transform.create(s0_Terrain_Amount_01__1__01, {
	position: {x:21.59,y:0,z:9.75},
	rotation: {x:0,y:0.9451896,z:0,w:0.3265221},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Terrain_Amount_02__1__01 //
const s0_Terrain_Amount_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Amount_02__1__01, {
	src: "unity_assets/s0_Terrain_Amount_02__1__01.glb",
})
Transform.create(s0_Terrain_Amount_02__1__01, {
	position: {x:2.93,y:0,z:58.68},
	rotation: {x:0,y:0.7071065,z:0,w:-0.7071071},
	scale: {x:1,y:0.6133,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Terrain_Amount_02__2__01 //
const s0_Terrain_Amount_02__2__01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Amount_02__2__01, {
	src: "unity_assets/s0_Terrain_Amount_02__1__01.glb",
})
Transform.create(s0_Terrain_Amount_02__2__01, {
	position: {x:12.68,y:0,z:51.04},
	rotation: {x:0,y:-0.4999999,z:0,w:-0.8660255},
	scale: {x:1,y:0.6133,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Terrain_Amount_02__3__01 //
const s0_Terrain_Amount_02__3__01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Amount_02__3__01, {
	src: "unity_assets/s0_Terrain_Amount_02__1__01.glb",
})
Transform.create(s0_Terrain_Amount_02__3__01, {
	position: {x:5.315723,y:0,z:82.29528},
	rotation: {x:0,y:-0.4999999,z:0,w:-0.8660255},
	scale: {x:1,y:0.6133,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Terrain_Amount_02__4__01 //
const s0_Terrain_Amount_02__4__01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Amount_02__4__01, {
	src: "unity_assets/s0_Terrain_Amount_02__1__01.glb",
})
Transform.create(s0_Terrain_Amount_02__4__01, {
	position: {x:5.58,y:0,z:87.54},
	rotation: {x:0,y:-0.7933531,z:0,w:-0.6087617},
	scale: {x:1,y:0.6133,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Terrain_Amount_02__5__01 //
const s0_Terrain_Amount_02__5__01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Amount_02__5__01, {
	src: "unity_assets/s0_Terrain_Amount_02__1__01.glb",
})
Transform.create(s0_Terrain_Amount_02__5__01, {
	position: {x:8.42,y:0,z:90.46},
	rotation: {x:0,y:-0.9238791,z:0,w:-0.3826843},
	scale: {x:1,y:0.6133,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Terrain_Amount_02__6__01 //
const s0_Terrain_Amount_02__6__01 = engine.addEntity()
GltfContainer.create(s0_Terrain_Amount_02__6__01, {
	src: "unity_assets/s0_Terrain_Amount_02__1__01.glb",
})
Transform.create(s0_Terrain_Amount_02__6__01, {
	position: {x:12.16,y:0,z:92.26},
	rotation: {x:0,y:-0.9659256,z:0,w:-0.2588202},
	scale: {x:1,y:0.6133,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Tree_01__1__01 //
const s0_Tree_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Tree_01__1__01, {
	src: "unity_assets/s0_Tree_01__1__01.glb",
})
Transform.create(s0_Tree_01__1__01, {
	position: {x:13.8,y:0,z:64.43},
	rotation: {x:0,y:0.7933542,z:0,w:-0.6087605},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Tree_02__1__01 //
const s0_Tree_02__1__01 = engine.addEntity()
GltfContainer.create(s0_Tree_02__1__01, {
	src: "unity_assets/s0_Tree_02__1__01.glb",
})
Transform.create(s0_Tree_02__1__01, {
	position: {x:13.53,y:8.82,z:86.12},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:0.7537332,y:0.726,z:0.726},
	parent: s0_Environment_01
})


// Entity: s0_Tree_02__2__01 //
const s0_Tree_02__2__01 = engine.addEntity()
GltfContainer.create(s0_Tree_02__2__01, {
	src: "unity_assets/s0_Tree_02__1__01.glb",
})
Transform.create(s0_Tree_02__2__01, {
	position: {x:20.2,y:-0.39,z:69.95},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:0.5072398,y:0.4885762,z:0.3649078},
	parent: s0_Environment_01
})


// Entity: s0_Tree_03__1__01 //
const s0_Tree_03__1__01 = engine.addEntity()
GltfContainer.create(s0_Tree_03__1__01, {
	src: "unity_assets/s0_Tree_03__1__01.glb",
})
Transform.create(s0_Tree_03__1__01, {
	position: {x:14.12,y:16.63,z:58.24},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1.99,y:1.99,z:1.99},
	parent: s0_Environment_01
})


// Entity: s0_Tree_03__2__01 //
const s0_Tree_03__2__01 = engine.addEntity()
GltfContainer.create(s0_Tree_03__2__01, {
	src: "unity_assets/s0_Tree_03__1__01.glb",
})
Transform.create(s0_Tree_03__2__01, {
	position: {x:20.45,y:16.63,z:47.97},
	rotation: {x:0,y:0.9238798,z:0,w:-0.3826828},
	scale: {x:1.233661,y:1.233661,z:1.233661},
	parent: s0_Environment_01
})


// Entity: s0_Tree_Trunk_01__1__01 //
const s0_Tree_Trunk_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Tree_Trunk_01__1__01, {
	src: "unity_assets/s0_Tree_Trunk_01__1__01.glb",
})
Transform.create(s0_Tree_Trunk_01__1__01, {
	position: {x:3.76,y:0,z:52.61},
	rotation: {x:0,y:1,z:0,w:-4.371139E-08},
	scale: {x:1,y:1,z:1},
	parent: s0_Environment_01
})


// Entity: s0_Water_Fall_01__1__01 //
const s0_Water_Fall_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Water_Fall_01__1__01, {
	src: "unity_assets/s0_Water_Fall_01__1__01.glb",
})
Transform.create(s0_Water_Fall_01__1__01, {
	position: {x:18.91,y:16.32,z:49.92},
	rotation: {x:0,y:0.8818489,z:0,w:0.4715322},
	scale: {x:1,y:1,z:0.5124},
	parent: s0_Environment_01
})


// Entity: s0_Water_Pond_01__1__01 //
const s0_Water_Pond_01__1__01 = engine.addEntity()
GltfContainer.create(s0_Water_Pond_01__1__01, {
	src: "unity_assets/s0_Water_Pond_01__1__01.glb",
})
Transform.create(s0_Water_Pond_01__1__01, {
	position: {x:19.62,y:16.36,z:54.35},
	rotation: {x:0,y:0.9509926,z:0,w:0.3092137},
	scale: {x:0.9678,y:1,z:0.9741},
	parent: s0_Environment_01
})


// Entity: s0_Water_Pond_01__2__01 //
const s0_Water_Pond_01__2__01 = engine.addEntity()
GltfContainer.create(s0_Water_Pond_01__2__01, {
	src: "unity_assets/s0_Water_Pond_01__1__01.glb",
})
Transform.create(s0_Water_Pond_01__2__01, {
	position: {x:6.526,y:0.187,z:102.092},
	rotation: {x:0,y:0.4538058,z:0,w:0.8911006},
	scale: {x:0.460229,y:0.4755415,z:0.4632249},
	parent: s0_Environment_01
})


// Entity: s0_TriggerArea_01 //
const s0_TriggerArea_01 = engine.addEntity()
Transform.create(s0_TriggerArea_01, {
	position: {x:15.36,y:1.5,z:4.48},
	rotation: {x:0,y:0,z:0,w:1},
	scale: {x:4,y:3,z:4}
})
utils.triggers.addTrigger(s0_TriggerArea_01,
	utils.NO_LAYERS,
	utils.LAYER_1,
	[{  type: 'box',
		scale: {x:4,y:3,z:4},
	}],
	function(){	//Enter
		movePlayerTo({ newRelativePosition: {x:17.87,y:19.49407,z:54.84}, cameraTarget: {x:15.36,y:1.5,z:4.48}})
	},
	function(){	//Exit
		
	}
)


// Entity: s0_GameObject_01 //
const s0_GameObject_01 = engine.addEntity()
Transform.create(s0_GameObject_01, {
	position: {x:17.87,y:19.49407,z:54.84},
	rotation: {x:0,y:0,z:0,w:1},
	scale: {x:1,y:1,z:1}
})

